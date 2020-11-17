import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminGuard } from '@base/app/core/guards/admin.guard';
import { AuthGuard } from '@core/guards/auth.guard';
import { TechnicalEmployeeGuard } from '@core/guards/technical-employee.guard';
import { DashboardLayoutComponent } from '@shared/layouts/dashboard-layout/dashboard-layout.component';
import { WorkOrderDetailComponent } from './components/work-order-detail/work-order-detail.component';
import { WorkOrderRegisterComponent } from './components/work-order-register/work-order-register.component';
import { WorkOrdersComponent } from './components/work-orders/work-orders.component';

const routes: Routes = [
  {
    path: '',
    component: DashboardLayoutComponent,
    canActivate: [ AuthGuard ],
    children: [
      { path: '', component: WorkOrdersComponent, canActivate: [ AdminGuard ] },
      { path: 'register', component: WorkOrderRegisterComponent, canActivate: [ TechnicalEmployeeGuard ] },
      { path: ':code', component: WorkOrderDetailComponent, canActivate: [ AdminGuard ] }
    ]
  }
];

@NgModule({
  imports: [ RouterModule.forChild(routes) ],
  exports: [ RouterModule ]
})
export class WorkOrdersRoutingModule {}
