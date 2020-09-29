import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardLayoutComponent } from '@app/shared/layouts/dashboard-layout/dashboard-layout.component';
import { AdminOrOfficeEmployeeGuard } from '@core/guards/admin-or-office-employee.guard';
import { AuthGuard } from '@core/guards/auth.guard';
import { ActivityDetailComponent } from './components/activity-detail/activity-detail.component';
import { ActivityRegisterComponent } from './components/activity-register/activity-register.component';
import { ActivityScheduleComponent } from './components/activity-schedule/activity-schedule.component';
import { ClientScheduleComponent } from './components/client-schedule/client-schedule.component';
import { WorkScheduleComponent } from './components/work-schedule/work-schedule.component';

const routes: Routes = [
  {
    path: '',
    component: DashboardLayoutComponent,
    children: [
      { path: '', component: ActivityScheduleComponent, canActivate: [ AuthGuard, AdminOrOfficeEmployeeGuard ] },
      {
        path: 'register',
        component: ActivityRegisterComponent,
        canActivate: [ AuthGuard, AdminOrOfficeEmployeeGuard ]
      },
      {
        path: 'work_schedule',
        component: WorkScheduleComponent,
        canActivate: [ AuthGuard ]
      },
      {
        path: 'client_schedule',
        component: ClientScheduleComponent,
        canActivate: [ AuthGuard ]
      },
      { path: ':code', component: ActivityDetailComponent, canActivate: [ AuthGuard ] }
    ]
  }
];

@NgModule({
  imports: [ RouterModule.forChild(routes) ],
  exports: [ RouterModule ]
})
export class ActivityScheduleRoutingModule {}
