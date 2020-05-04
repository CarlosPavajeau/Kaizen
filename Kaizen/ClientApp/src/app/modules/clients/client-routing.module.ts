import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ClientRegisterComponent } from './components/client-register/client-register.component';
import { ClientsComponent } from './components/clients/clients.component';
import { AdminGuard } from '@core/guards/admin.guard';
import { AuthGuard } from '@core/guards/auth.guard';

const routes: Routes = [
	{
		path: '',
		children: [
			{ path: '', component: ClientsComponent, canActivate: [ AuthGuard, AdminGuard ] },
			{ path: 'register', component: ClientRegisterComponent }
		]
	}
];

@NgModule({
	imports: [ RouterModule.forChild(routes) ],
	exports: [ RouterModule ]
})
export class ClientRoutingModule {}
