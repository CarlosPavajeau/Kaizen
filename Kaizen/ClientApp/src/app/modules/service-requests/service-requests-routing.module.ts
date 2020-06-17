import { AdminOrOfficeEmployeeGuard } from '@core/guards/admin-or-office-employee.guard';
import { AuthGuard } from '@core/guards/auth.guard';
import { ClientGuard } from '@core/guards/client.guard';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ServiceRequestDetailComponent } from './components/service-request-detail/service-request-detail.component';
import { ServiceRequestRegisterComponent } from './components/service-request-register/service-request-register.component';
import { ServiceRequestsComponent } from './components/service-requests/service-requests.component';

const routes: Routes = [
	{
		path: '',
		children: [
			{
				path: '',
				component: ServiceRequestsComponent,
				canActivate: [ AuthGuard, AdminOrOfficeEmployeeGuard ]
			},
			{ path: 'register', component: ServiceRequestRegisterComponent, canActivate: [ AuthGuard, ClientGuard ] },
			{
				path: ':code',
				component: ServiceRequestDetailComponent,
				canActivate: [ AuthGuard, AdminOrOfficeEmployeeGuard ]
			}
		]
	}
];

@NgModule({
	imports: [ RouterModule.forChild(routes) ],
	exports: [ RouterModule ]
})
export class ServiceRequestsRoutingModule {}
