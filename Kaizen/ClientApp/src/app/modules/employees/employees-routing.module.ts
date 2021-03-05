import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminGuard } from '@core/guards/admin.guard';
import { AuthGuard } from '@core/guards/auth.guard';
import { DashboardLayoutComponent } from '@shared/layouts/dashboard-layout/dashboard-layout.component';
import { EmployeeChargesComponent } from './components/employee-charges/employee-charges.component';
import { EmployeeDetailComponent } from './components/employee-detail/employee-detail.component';
import { EmployeeEditComponent } from './components/employee-edit/employee-edit.component';
import { EmployeeMapComponent } from './components/employee-map/employee-map.component';
import { EmployeeRegisterComponent } from './components/employee-register/employee-register.component';
import { EmployeesComponent } from './components/employees/employees.component';

const routes: Routes = [
  {
    path: '',
    canActivate: [ AuthGuard, AdminGuard ],
    component: DashboardLayoutComponent,
    children: [
      {
        path: '',
        component: EmployeesComponent
      },
      {
        path: 'map',
        component: EmployeeMapComponent
      },
      {
        path: 'charges',
        component: EmployeeChargesComponent
      },
      {
        path: 'register',
        component: EmployeeRegisterComponent
      },
      {
        path: 'edit/:id',
        component: EmployeeEditComponent
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
export class EmployeesRoutingModule {
}
