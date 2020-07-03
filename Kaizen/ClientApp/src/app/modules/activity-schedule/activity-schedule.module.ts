import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ActivityScheduleRoutingModule } from './activity-schedule-routing.module';
import { ActivityRegisterComponent } from './components/activity-register/activity-register.component';
import { ActivityScheduleComponent } from './components/activity-schedule/activity-schedule.component';
import { ActivityDetailComponent } from './components/activity-detail/activity-detail.component';
import { SharedModule } from '@app/shared/shared.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ServiceRequestsModule } from '../service-requests/service-requests.module';
import { ActivityDetailCardComponent } from './components/activity-detail-card/activity-detail-card.component';
import { ActivityButtonComponent } from './components/activity-button/activity-button.component';
import { ActivityScheduleDayComponent } from './components/activity-schedule-day/activity-schedule-day.component';
import { ActivityScheduleMonthComponent } from './components/activity-schedule-month/activity-schedule-month.component';

@NgModule({
	declarations: [
		ActivityRegisterComponent,
		ActivityScheduleComponent,
		ActivityDetailComponent,
		ActivityDetailCardComponent,
		ActivityButtonComponent,
		ActivityScheduleDayComponent,
		ActivityScheduleMonthComponent
	],
	imports: [ SharedModule, ActivityScheduleRoutingModule, FormsModule, ReactiveFormsModule, ServiceRequestsModule ]
})
export class ActivityScheduleModule {}
