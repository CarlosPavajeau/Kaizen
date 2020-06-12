import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { WorkOrdersRoutingModule } from './work-orders-routing.module';
import { WorkOrderRegisterComponent } from './components/work-order-register/work-order-register.component';
import { WorkOrderDetailComponent } from './components/work-order-detail/work-order-detail.component';


@NgModule({
  declarations: [WorkOrderRegisterComponent, WorkOrderDetailComponent],
  imports: [
    CommonModule,
    WorkOrdersRoutingModule
  ]
})
export class WorkOrdersModule { }
