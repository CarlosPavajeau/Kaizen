import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminGuard } from '@core/guards/admin.guard';
import { AuthGuard } from '@core/guards/auth.guard';
import { DashboardLayoutComponent } from '@shared/layouts/dashboard-layout/dashboard-layout.component';
import { ServiceDetailComponent } from './components/service-detail/service-detail.component';
import { ServiceEditComponent } from './components/service-edit/service-edit.component';
import { ServiceRegisterComponent } from './components/service-register/service-register.component';
import { ServicesComponent } from './components/services/services.component';

const routes: Routes = [
  {
    path: '',
    component: DashboardLayoutComponent,
    canActivate: [ AuthGuard, AdminGuard ],
    children: [
      { path: '', component: ServicesComponent },
      { path: 'register', component: ServiceRegisterComponent },
      { path: 'edit/:code', component: ServiceEditComponent },
      { path: ':code', component: ServiceDetailComponent }
    ]
  }
];

@NgModule({
  imports: [ RouterModule.forChild(routes) ],
  exports: [ RouterModule ]
})
export class ServicesRoutingModule {}
