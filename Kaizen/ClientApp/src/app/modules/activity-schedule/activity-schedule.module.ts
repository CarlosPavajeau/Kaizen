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

@NgModule({
	declarations: [ ActivityRegisterComponent, ActivityScheduleComponent, ActivityDetailComponent, ActivityDetailCardComponent, ActivityButtonComponent ],
	imports: [ SharedModule, ActivityScheduleRoutingModule, FormsModule, ReactiveFormsModule, ServiceRequestsModule ]
})
export class ActivityScheduleModule {}
