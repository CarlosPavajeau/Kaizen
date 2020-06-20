import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { SharedModule } from '@shared/shared.module';
import { WorkOrderDetailComponent } from './components/work-order-detail/work-order-detail.component';
import { WorkOrderRegisterComponent } from './components/work-order-register/work-order-register.component';
import { WorkOrdersRoutingModule } from './work-orders-routing.module';

@NgModule({
	declarations: [ WorkOrderRegisterComponent, WorkOrderDetailComponent ],
	imports: [ SharedModule, WorkOrdersRoutingModule, FormsModule, ReactiveFormsModule ]
})
export class WorkOrdersModule {}
