import { DOCUMENT } from '@angular/common';
import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_MOMENT_DATE_ADAPTER_OPTIONS, MomentDateAdapter } from '@angular/material-moment-adapter';
import { DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE } from '@angular/material/core';
import { MatDatepicker } from '@angular/material/datepicker';
import { ActivatedRoute, Router } from '@angular/router';
import { IForm } from '@app/core/models/form';
import { NotificationsService } from '@app/shared/services/notifications.service';
import { PayModel } from '@modules/payments/models/pay';
import { ServiceInvoice } from '@modules/payments/models/service-invoice';
import { PaymentService } from '@modules/payments/services/payment.service';
import { ServiceInvoiceService } from '@modules/payments/services/service-invoice.service';
import { ObservableStatus } from '@shared/models/observable-with-status';
import * as _moment from 'moment';
// tslint:disable-next-line:no-duplicate-imports
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
  selector: 'app-pay-service-invoice',
  templateUrl: './pay-service-invoice.component.html',
  styleUrls: [ './pay-service-invoice.component.scss' ],
  providers: [
    {
      provide: DateAdapter,
      useClass: MomentDateAdapter,
      deps: [ MAT_DATE_LOCALE, MAT_MOMENT_DATE_ADAPTER_OPTIONS ]
    },

    { provide: MAT_DATE_FORMATS, useValue: DATE_FORMATS }
  ]
})
export class PayServiceInvoiceComponent implements OnInit, IForm, OnDestroy {
  public ObsStatus: typeof ObservableStatus = ObservableStatus;

  serviceInvoice: ServiceInvoice;
  serviceInvoice$: Observable<ServiceInvoice>;

  payForm: FormGroup;

  payModel: PayModel;
  paymentInProcess = false;

  get controls(): { [key: string]: AbstractControl } {
    return this.payForm.controls;
  }

  constructor(
    private serviceInvoiceService: ServiceInvoiceService,
    private paymentService: PaymentService,
    private formBuilder: FormBuilder,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private notificationsService: NotificationsService,
    @Inject(DOCUMENT) private _document: Document
  ) {}

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
    const id = +this.activatedRoute.snapshot.paramMap.get('id');
    this.serviceInvoice$ = this.serviceInvoiceService.getServiceInvoice(id);
    this.serviceInvoice$.subscribe((serviceInvoice) => {
      this.serviceInvoice = serviceInvoice;
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
        transactionAmount: this.serviceInvoice.total
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
          this.serviceInvoiceService
            .payServiceInvoice(this.serviceInvoice, paymentModel)
            .subscribe((serviceInvoice: ServiceInvoice) => {
              if (serviceInvoice) {
                this.notificationsService.showSuccessMessage(
                  'Pago processado correctamente, su factura ha sido pagada.',
                  () => {
                    this.router.navigateByUrl('/payments/service_invoices');
                  }
                );
              }
            });
        });
    }
  }
}
