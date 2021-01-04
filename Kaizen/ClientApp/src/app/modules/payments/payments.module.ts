import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from '@shared/shared.module';
import { PayServiceInvoiceComponent } from './components/pay-service-invoice/pay-service-invoice.component';
import { PaymentRegisterComponent } from './components/payment-register/payment-register.component';
import { ProductInvoicesComponent } from './components/product-invoices/product-invoices.component';
import { ServiceInvoicesComponent } from './components/service-invoices/service-invoices.component';
import { PaymentsRoutingModule } from './payments-routing.module';
import { ServiceInvoiceDetailComponent } from './components/service-invoice-detail/service-invoice-detail.component';
import { PayProductInvoiceComponent } from './components/pay-product-invoice/pay-product-invoice.component';
import { ProductInvoiceRegisterComponent } from './components/product-invoice-register/product-invoice-register.component';

@NgModule({
  declarations: [
    PaymentRegisterComponent,
    ServiceInvoicesComponent,
    ProductInvoicesComponent,
    PayServiceInvoiceComponent,
    ServiceInvoiceDetailComponent,
    PayProductInvoiceComponent,
    ProductInvoiceRegisterComponent
  ],
  imports: [ CommonModule, PaymentsRoutingModule, SharedModule, ReactiveFormsModule, FormsModule ]
})
export class PaymentsModule {}
