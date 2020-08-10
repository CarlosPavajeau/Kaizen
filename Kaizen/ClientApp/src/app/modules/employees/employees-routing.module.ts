import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { EmployeeRegisterComponent } from './components/employee-register/employee-register.component';
import { EmployeesComponent } from './components/employees/employees.component';
import { AuthGuard } from '@core/guards/auth.guard';
import { AdminGuard } from '@core/guards/admin.guard';
import { EmployeeDetailComponent } from './components/employee-detail/employee-detail.component';

const routes: Routes = [
  {
    path: '',
    canActivate: [ AuthGuard, AdminGuard ],
    children: [
      {
        path: '',
        component: EmployeesComponent
      },
      {
        path: 'register',
        component: EmployeeRegisterComponent
      },
      {
        path: ':id',
        component: EmployeeDetailComponent
      }
    ]
  }
];

@NgModule({
  imports: [ RouterModule.forChild(routes) ],
  exports: [ RouterModule ]
})
export class EmployeesRoutingModule {}
