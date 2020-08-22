import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { SharedModule } from '@shared/shared.module';
import { PaymentRegisterComponent } from './components/payment-register/payment-register.component';
import { ProductInvoicesComponent } from './components/product-invoices/product-invoices.component';
import { ServiceInvoicesComponent } from './components/service-invoices/service-invoices.component';
import { PaymentsRoutingModule } from './payments-routing.module';

@NgModule({
  declarations: [ PaymentRegisterComponent, ServiceInvoicesComponent, ProductInvoicesComponent ],
  imports: [ CommonModule, PaymentsRoutingModule, SharedModule ]
})
export class PaymentsModule {}
