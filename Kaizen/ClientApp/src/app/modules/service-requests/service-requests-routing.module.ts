import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ServiceRequestsComponent } from './components/service-requests/service-requests.component';
import { AuthGuard } from '@app/core/guards/auth.guard';
import { AdminGuard } from '@app/core/guards/admin.guard';
import { ServiceRequestRegisterComponent } from './components/service-request-register/service-request-register.component';
import { ServiceRequestDetailComponent } from './components/service-request-detail/service-request-detail.component';

const routes: Routes = [
	{
		path: '',
		children: [
			{ path: '', component: ServiceRequestsComponent, canActivate: [ AuthGuard, AdminGuard ] },
			{ path: 'register', component: ServiceRequestRegisterComponent, canActivate: [ AuthGuard ] },
			{ path: '/:id', component: ServiceRequestDetailComponent, canActivate: [ AuthGuard, AdminGuard ] }
		]
	}
];

@NgModule({
	imports: [ RouterModule.forChild(routes) ],
	exports: [ RouterModule ]
})
export class ServiceRequestsRoutingModule {}
