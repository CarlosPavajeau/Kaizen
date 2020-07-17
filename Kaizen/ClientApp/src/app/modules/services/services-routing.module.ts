import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from '@core/guards/auth.guard';
import { ServicesComponent } from './components/services/services.component';
import { ServiceRegisterComponent } from './components/service-register/service-register.component';
import { AdminGuard } from '@core/guards/admin.guard';
import { ServiceDetailComponent } from './components/service-detail/service-detail.component';

const routes: Routes = [
	{
		path: '',
		canActivate: [ AuthGuard, AdminGuard ],
		children: [
			{ path: '', component: ServicesComponent },
			{ path: 'register', component: ServiceRegisterComponent },
			{ path: ':code', component: ServiceDetailComponent }
		]
	}
];

@NgModule({
	imports: [ RouterModule.forChild(routes) ],
	exports: [ RouterModule ]
})
export class ServicesRoutingModule {}
