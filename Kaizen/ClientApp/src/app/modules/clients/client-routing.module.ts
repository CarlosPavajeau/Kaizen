import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ClientRegisterComponent } from './components/client-register/client-register.component';
import { ClientsComponent } from './components/clients/clients.component';
import { AdminGuard } from '@core/guards/admin.guard';
import { AuthGuard } from '@core/guards/auth.guard';
import { ClientRequestsComponent } from './components/client-requests/client-requests.component';
import { ClientRequestDetailComponent } from './components/client-request-detail/client-request-detail.component';
import { ClientRegisterGuard } from '@app/core/guards/client-register.guard';
import { AdminOrOfficeEmployeeGuard } from '@app/core/guards/admin-or-office-employee.guard';
import { ClientDetailComponent } from './components/client-detail/client-detail.component';

const routes: Routes = [
  {
    path: '',
    children: [
      { path: '', component: ClientsComponent, canActivate: [ AuthGuard, AdminGuard ] },
      { path: 'register', component: ClientRegisterComponent, canActivate: [ ClientRegisterGuard ] },
      {
        path: 'requests',
        component: ClientRequestsComponent,
        canActivate: [ AuthGuard, AdminOrOfficeEmployeeGuard ]
      },
      {
        path: 'requests/:id',
        component: ClientRequestDetailComponent,
        canActivate: [ AuthGuard, AdminOrOfficeEmployeeGuard ]
      },
      {
        path: ':id',
        component: ClientDetailComponent,
        canActivate: [ AuthGuard, AdminOrOfficeEmployeeGuard ]
      }
    ]
  }
];

@NgModule({
  imports: [ RouterModule.forChild(routes) ],
  exports: [ RouterModule ]
})
export class ClientRoutingModule {}
