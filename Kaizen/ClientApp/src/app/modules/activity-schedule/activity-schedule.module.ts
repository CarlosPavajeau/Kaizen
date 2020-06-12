import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ActivityScheduleRoutingModule } from './activity-schedule-routing.module';
import { ActivityRegisterComponent } from './components/activity-register/activity-register.component';
import { ActivityScheduleComponent } from './components/activity-schedule/activity-schedule.component';
import { ActivityDetailComponent } from './components/activity-detail/activity-detail.component';


@NgModule({
  declarations: [ActivityRegisterComponent, ActivityScheduleComponent, ActivityDetailComponent],
  imports: [
    CommonModule,
    ActivityScheduleRoutingModule
  ]
})
export class ActivityScheduleModule { }
