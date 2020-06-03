import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ClientRegisterComponent } from './components/client-register/client-register.component';
import { ClientsComponent } from './components/clients/clients.component';
import { AdminGuard } from '@core/guards/admin.guard';
import { AuthGuard } from '@core/guards/auth.guard';
import { ClientRequestsComponent } from './components/client-requests/client-requests.component';
import { ClientRequestDetailComponent } from './components/client-request-detail/client-request-detail.component';

const routes: Routes = [
	{
		path: '',
		children: [
			{ path: '', component: ClientsComponent, canActivate: [ AuthGuard, AdminGuard ] },
			{ path: 'register', component: ClientRegisterComponent },
			{ path: 'requests', component: ClientRequestsComponent, canActivate: [ AuthGuard, AdminGuard ] },
			{ path: 'requests/:id', component: ClientRequestDetailComponent, canActivate: [ AuthGuard, AdminGuard ] }
		]
	}
];

@NgModule({
	imports: [ RouterModule.forChild(routes) ],
	exports: [ RouterModule ]
})
export class ClientRoutingModule {}
