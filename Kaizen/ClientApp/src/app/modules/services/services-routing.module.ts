import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from '@app/core/guards/auth.guard';
import { ServicesComponent } from './components/services/services.component';
import { ServiceRegisterComponent } from './components/service-register/service-register.component';

const routes: Routes = [
	{
		path: '',
		canActivate: [ AuthGuard ],
		children: [
			{ path: '', component: ServicesComponent },
			{ path: 'register', component: ServiceRegisterComponent }
		]
	}
];

@NgModule({
	imports: [ RouterModule.forChild(routes) ],
	exports: [ RouterModule ]
})
export class ServicesRoutingModule {}
