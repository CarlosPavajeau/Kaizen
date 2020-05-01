import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { EmployeeRegisterComponent } from './components/employee-register/employee-register.component';
import { EmployeesComponent } from './components/employees/employees.component';

const routes: Routes = [
	{
		path: '',
		children: [
			{ path: 'register', component: EmployeeRegisterComponent },
			{ path: 'employees', component: EmployeesComponent },
			{ path: '', redirectTo: 'employees', pathMatch: 'full' }
		]
	}
];

@NgModule({
	imports: [ RouterModule.forChild(routes) ],
	exports: [ RouterModule ]
})
export class EmployeesRoutingModule {}
