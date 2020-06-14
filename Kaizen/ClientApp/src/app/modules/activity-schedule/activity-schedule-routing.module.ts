import { ActivityDetailComponent } from './components/activity-detail/activity-detail.component';
import { ActivityRegisterComponent } from './components/activity-register/activity-register.component';
import { ActivityScheduleComponent } from './components/activity-schedule/activity-schedule.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
	{
		path: '',
		children: [
			{ path: '', component: ActivityScheduleComponent },
			{ path: 'register', component: ActivityRegisterComponent },
			{ path: ':code', component: ActivityDetailComponent }
		]
	}
];

@NgModule({
	imports: [ RouterModule.forChild(routes) ],
	exports: [ RouterModule ]
})
export class ActivityScheduleRoutingModule {}
