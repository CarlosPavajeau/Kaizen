import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ClientRegisterComponent } from './components/client-register/client-register.component';
import { ClientsComponent } from './components/clients/clients.component';

const routes: Routes = [
	{
		path: '',
		children: [
			{ path: 'register', component: ClientRegisterComponent },
			{ path: 'clients', component: ClientsComponent },
			{ path: '', redirectTo: '/', pathMatch: 'full' }
		]
	}
];

@NgModule({
	imports: [ RouterModule.forChild(routes) ],
	exports: [ RouterModule ]
})
export class ClientRoutingModule {}
