import { DOCUMENT } from '@angular/common';
import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_MOMENT_DATE_ADAPTER_OPTIONS, MomentDateAdapter } from '@angular/material-moment-adapter';
import { DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE } from '@angular/material/core';
import { MatDatepicker } from '@angular/material/datepicker';
import { ActivatedRoute, Router } from '@angular/router';
import { IForm } from '@core/models/form';
import { PayModel } from '@modules/payments/models/pay';
import { ProductInvoice } from '@modules/payments/models/product-invoice';
import { PaymentService } from '@modules/payments/services/payment.service';
import { ProductInvoiceService } from '@modules/payments/services/product-invoice.service';
import { ObservableStatus } from '@shared/models/observable-with-status';
import { DialogsService } from '@shared/services/dialogs.service';
import * as _moment from 'moment';
import { Moment } from 'moment';
import { Observable, of } from 'rxjs';
import { switchMap } from 'rxjs/operators';

const moment = _moment;

export const DATE_FORMATS = {
  parse: {
    dateInput: 'MM/YYYY'
  },
  display: {
    dateInput: 'MM/YYYY',
    monthYearLabel: 'MMM YYYY',
    dateA11yLabel: 'LL',
    monthYearA11yLabel: 'MMMM YYYY'
  }
};

@Component({
  selector: 'app-pay-product-invoice',
  templateUrl: './pay-product-invoice.component.html',
  providers: [
    {
      provide: DateAdapter,
      useClass: MomentDateAdapter,
      deps: [ MAT_DATE_LOCALE, MAT_MOMENT_DATE_ADAPTER_OPTIONS ]
    },

    { provide: MAT_DATE_FORMATS, useValue: DATE_FORMATS }
  ]
})
export class PayProductInvoiceComponent implements OnInit, IForm, OnDestroy {
  public ObsStatus: typeof ObservableStatus = ObservableStatus;

  productInvoice: ProductInvoice;
  productInvoice$: Observable<ProductInvoice>;

  payForm: FormGroup;
  payModel: PayModel;
  paymentInProcess = false;

  get controls(): { [key: string]: AbstractControl } {
    return this.payForm.controls;
  }

  constructor(
    private productInvoiceService: ProductInvoiceService,
    private paymentService: PaymentService,
    private formBuilder: FormBuilder,
    private activatedRouter: ActivatedRoute,
    private router: Router,
    private dialogsService: DialogsService,
    @Inject(DOCUMENT) private _document: Document
  ) {
  }

  ngOnInit(): void {
    this.initForm();
    this.loadData();
    this._document.body.classList.add('royal_azure');
  }

  ngOnDestroy(): void {
    this._document.body.classList.remove('royal_azure');
  }

  initForm(): void {
    this.payForm = this.formBuilder.group({
      cardNumber: [ '', [ Validators.required ] ],
      cardholderName: [ '', [ Validators.required ] ],
      cardExpirationDate: [ moment(), [ Validators.required ] ],
      securityCode: [ '', [ Validators.required, Validators.maxLength(3) ] ],
      docType: [ '', [ Validators.required ] ],
      docNumber: [ '', [ Validators.required ] ],
      email: [ '', [ Validators.required, Validators.email ] ]
    });
  }

  private loadData(): void {
    const id = +this.activatedRouter.snapshot.paramMap.get('id');
    this.productInvoice$ = this.productInvoiceService.getProductInvoice(id);

    this.productInvoice$.subscribe((productInvoice) => {
      this.productInvoice = productInvoice;
    });
  }

  chosenYearHandler(normalizedYear: Moment) {
    const ctrlValue = this.controls['cardExpirationDate'].value;
    ctrlValue.year(normalizedYear.year());
    this.controls['cardExpirationDate'].setValue(ctrlValue);
  }

  chosenMonthHandler(normalizedMonth: Moment, datepicker: MatDatepicker<Moment>) {
    const ctrlValue = this.controls['cardExpirationDate'].value;
    ctrlValue.month(normalizedMonth.month());
    this.controls['cardExpirationDate'].setValue(ctrlValue);
    datepicker.close();
  }

  onSubmit(): void {
    if (this.payForm.valid) {
      this.paymentInProcess = true;

      this.payModel = {
        ...this.payForm.value,
        transactionAmount: this.productInvoice.total
      };

      this.paymentService
        .getPaymentMethod(this.payModel.cardNumber)
        .pipe(
          switchMap((paymentMethod: string) => {
            this.payModel.paymentMethodId = paymentMethod;

            const expirationDate = this.payForm.controls['cardExpirationDate'].value as Moment;
            this.payModel.cardExpirationMonth = (expirationDate.month() + 1).toString();
            this.payModel.cardExpirationYear = expirationDate.year().toString();
            return this.paymentService.tokenizeCard(this.payModel);
          }),
          switchMap((cardToken: string) => {
            this.payModel.token = cardToken;
            return of(this.payModel);
          })
        )
        .subscribe((paymentModel: PayModel) => {
          console.log(paymentModel);
          this.productInvoiceService
            .payProductInvoice(this.productInvoice, paymentModel)
            .subscribe((productInvoice: ProductInvoice) => {
              if (productInvoice) {
                this.dialogsService.showSuccessDialog(
                  'Pago processado correctamente, su factura ha sido pagada.',
                  () => {
                    this.router.navigateByUrl('/payments/invoices/products');
                  }
                );
              }
            });
        });
    }
  }
}
