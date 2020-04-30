import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { EmployeesRoutingModule } from './employees-routing.module';
import { EmployeeRegisterComponent } from './components/employee-register/employee-register.component';
import { EmployeesComponent } from './components/employees/employees.component';

@NgModule({
	declarations: [ EmployeeRegisterComponent, EmployeesComponent ],
	imports: [ CommonModule, EmployeesRoutingModule ]
})
export class EmployeesModule {}
