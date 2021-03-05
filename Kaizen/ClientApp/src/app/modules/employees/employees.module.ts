import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { GoogleMapsModule } from '@angular/google-maps';
import { UserModule } from '@modules/users/user.module';
import { SharedModule } from '@shared/shared.module';
import { EmployeeDetailComponent } from './components/employee-detail/employee-detail.component';
import { EmployeeEditComponent } from './components/employee-edit/employee-edit.component';
import { EmployeeRegisterComponent } from './components/employee-register/employee-register.component';
import { EmployeesComponent } from './components/employees/employees.component';
import { EmployeesRoutingModule } from './employees-routing.module';
import { EmployeeMapComponent } from './components/employee-map/employee-map.component';
import { EmployeeChargeRegisterComponent } from './components/employee-charge-register/employee-charge-register.component';
import { EmployeeChargesComponent } from './components/employee-charges/employee-charges.component';

@NgModule({
  declarations: [ EmployeeRegisterComponent, EmployeesComponent, EmployeeDetailComponent, EmployeeEditComponent, EmployeeMapComponent, EmployeeChargeRegisterComponent, EmployeeChargesComponent ],
  imports: [ EmployeesRoutingModule, SharedModule, UserModule, FormsModule, ReactiveFormsModule, GoogleMapsModule ]
})
export class EmployeesModule {
}
