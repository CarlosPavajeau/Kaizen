import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminOrOfficeEmployeeGuard } from '@core/guards/admin-or-office-employee.guard';
import { AuthGuard } from '@core/guards/auth.guard';
import { ClientGuard } from '@core/guards/client.guard';
import { DashboardLayoutComponent } from '@shared/layouts/dashboard-layout/dashboard-layout.component';
import { ServiceRequestDetailComponent } from './components/service-request-detail/service-request-detail.component';
import { ServiceRequestNewDateComponent } from './components/service-request-new-date/service-request-new-date.component';
import { ServiceRequestProcessComponent } from './components/service-request-process/service-request-process.component';
import { ServiceRequestRegisterComponent } from './components/service-request-register/service-request-register.component';
import { ServiceRequestsComponent } from './components/service-requests/service-requests.component';

const routes: Routes = [
  {
    path: '',
    component: DashboardLayoutComponent,
    children: [
      {
        path: '',
        component: ServiceRequestsComponent,
        canActivate: [ AuthGuard, AdminOrOfficeEmployeeGuard ]
      },
      { path: 'register', component: ServiceRequestRegisterComponent, canActivate: [ AuthGuard, ClientGuard ] },
      { path: 'new_date', component: ServiceRequestNewDateComponent, canActivate: [ AuthGuard, ClientGuard ] },
      {
        path: 'process/:code',
        component: ServiceRequestProcessComponent,
        canActivate: [ AuthGuard, AdminOrOfficeEmployeeGuard ]
      },
      {
        path: ':code',
        component: ServiceRequestDetailComponent,
        canActivate: [ AuthGuard ]
      }
    ]
  }
];

@NgModule({
  imports: [ RouterModule.forChild(routes) ],
  exports: [ RouterModule ]
})
export class ServiceRequestsRoutingModule {}
