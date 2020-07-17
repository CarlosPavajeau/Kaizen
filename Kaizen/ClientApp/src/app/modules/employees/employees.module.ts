import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { EmployeesRoutingModule } from './employees-routing.module';
import { EmployeeRegisterComponent } from './components/employee-register/employee-register.component';
import { EmployeesComponent } from './components/employees/employees.component';
import { SharedModule } from '@shared/shared.module';
import { UserModule } from '@modules/users/user.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { EmployeeDetailComponent } from './components/employee-detail/employee-detail.component';

@NgModule({
	declarations: [ EmployeeRegisterComponent, EmployeesComponent, EmployeeDetailComponent ],
	imports: [ EmployeesRoutingModule, SharedModule, UserModule, FormsModule, ReactiveFormsModule ]
})
export class EmployeesModule {}
