import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { IForm } from '@app/core/models/form';
import { NotificationsService } from '@app/shared/services/notifications.service';
import { PayModel } from '@modules/payments/models/pay';
import { ServiceInvoice } from '@modules/payments/models/service-invoice';
import { PaymentService } from '@modules/payments/services/payment.service';
import { ServiceInvoiceService } from '@modules/payments/services/service-invoice.service';
import { of } from 'rxjs';
import { switchMap } from 'rxjs/operators';

@Component({
  selector: 'app-pay-service-invoice',
  templateUrl: './pay-service-invoice.component.html',
  styleUrls: [ './pay-service-invoice.component.css' ]
})
export class PayServiceInvoiceComponent implements OnInit, IForm {
  serviceInvoice: ServiceInvoice;
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
    private notificationsService: NotificationsService
  ) {}

  ngOnInit(): void {
    this.initForm();
    this.loadData();
  }

  initForm(): void {
    this.payForm = this.formBuilder.group({
      cardNumber: [ '', [ Validators.required ] ],
      cardholderName: [ '', [ Validators.required ] ],
      cardExpirationDate: [ '', [ Validators.required ] ],
      securityCode: [ '', [ Validators.required ] ],
      docType: [ '', [ Validators.required ] ],
      docNumber: [ '', [ Validators.required ] ],
      email: [ '', [ Validators.required, Validators.email ] ]
    });
  }

  private loadData(): void {
    const id = +this.activatedRoute.snapshot.paramMap.get('id');
    this.serviceInvoiceService.getServiceInvoice(id).subscribe((serviceInvoice: ServiceInvoice) => {
      this.serviceInvoice = serviceInvoice;
    });
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

            const expirationDate = this.payForm.controls['cardExpirationDate'].value as Date;
            this.payModel.cardExpirationMonth = (expirationDate.getMonth() + 1).toString();
            this.payModel.cardExpirationYear = expirationDate.getFullYear().toString();
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
