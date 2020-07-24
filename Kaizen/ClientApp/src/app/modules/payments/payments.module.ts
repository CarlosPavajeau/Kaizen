import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PaymentsRoutingModule } from './payments-routing.module';
import { PaymentRegisterComponent } from './components/payment-register/payment-register.component';
import { SharedModule } from '@app/shared/shared.module';

@NgModule({
	declarations: [ PaymentRegisterComponent ],
	imports: [ CommonModule, PaymentsRoutingModule, SharedModule ]
})
export class PaymentsModule {}
