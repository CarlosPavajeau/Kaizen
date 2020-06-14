import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ActivityScheduleRoutingModule } from './activity-schedule-routing.module';
import { ActivityRegisterComponent } from './components/activity-register/activity-register.component';
import { ActivityScheduleComponent } from './components/activity-schedule/activity-schedule.component';
import { ActivityDetailComponent } from './components/activity-detail/activity-detail.component';
import { SharedModule } from '@app/shared/shared.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
	declarations: [ ActivityRegisterComponent, ActivityScheduleComponent, ActivityDetailComponent ],
	imports: [ SharedModule, ActivityScheduleRoutingModule, FormsModule, ReactiveFormsModule ]
})
export class ActivityScheduleModule {}
