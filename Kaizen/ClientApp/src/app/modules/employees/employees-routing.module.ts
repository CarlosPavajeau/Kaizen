import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { EmployeeRegisterComponent } from './components/employee-register/employee-register.component';
import { EmployeesComponent } from './components/employees/employees.component';
import { AuthGuard } from '@core/guards/auth.guard';
import { AdminGuard } from '@core/guards/admin.guard';

const routes: Routes = [
	{
		path: '',
		children: [
			{ path: 'register', component: EmployeeRegisterComponent, canActivate: [ AuthGuard, AdminGuard ] },
			{ path: 'employees', component: EmployeesComponent, canActivate: [ AuthGuard, AdminGuard ] },
			{ path: '', redirectTo: 'employees', pathMatch: 'full' }
		]
	}
];

@NgModule({
	imports: [ RouterModule.forChild(routes) ],
	exports: [ RouterModule ]
})
export class EmployeesRoutingModule {}
