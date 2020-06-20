import { AuthGuard } from '@core/guards/auth.guard';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TechnicalEmployeeGuard } from '@core/guards/technical-employee.guard';
import { WorkOrderDetailComponent } from './components/work-order-detail/work-order-detail.component';
import { WorkOrderRegisterComponent } from './components/work-order-register/work-order-register.component';

const routes: Routes = [
	{
		path: '',
		children: [
			{ path: ':code', component: WorkOrderDetailComponent, canActivate: [ AuthGuard ] },
			{ path: 'register', component: WorkOrderRegisterComponent, canActivate: [ TechnicalEmployeeGuard ] }
		]
	}
];

@NgModule({
	imports: [ RouterModule.forChild(routes) ],
	exports: [ RouterModule ]
})
export class WorkOrdersRoutingModule {}
