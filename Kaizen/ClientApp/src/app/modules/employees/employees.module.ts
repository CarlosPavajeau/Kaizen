import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { UserModule } from '@modules/users/user.module';
import { SharedModule } from '@shared/shared.module';
import { EmployeeDetailComponent } from './components/employee-detail/employee-detail.component';
import { EmployeeEditComponent } from './components/employee-edit/employee-edit.component';
import { EmployeeRegisterComponent } from './components/employee-register/employee-register.component';
import { EmployeesComponent } from './components/employees/employees.component';
import { EmployeesRoutingModule } from './employees-routing.module';

@NgModule({
  declarations: [ EmployeeRegisterComponent, EmployeesComponent, EmployeeDetailComponent, EmployeeEditComponent ],
  imports: [ EmployeesRoutingModule, SharedModule, UserModule, FormsModule, ReactiveFormsModule ]
})
export class EmployeesModule {}
