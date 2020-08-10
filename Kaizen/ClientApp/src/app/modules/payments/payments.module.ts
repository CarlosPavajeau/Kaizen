import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PaymentsRoutingModule } from './payments-routing.module';
import { PaymentRegisterComponent } from './components/payment-register/payment-register.component';
import { SharedModule } from '@app/shared/shared.module';
import { ServiceInvoicesComponent } from './components/service-invoices/service-invoices.component';
import { ProductInvoicesComponent } from './components/product-invoices/product-invoices.component';

@NgModule({
  declarations: [ PaymentRegisterComponent, ServiceInvoicesComponent, ProductInvoicesComponent ],
  imports: [ CommonModule, PaymentsRoutingModule, SharedModule ]
})
export class PaymentsModule {}
