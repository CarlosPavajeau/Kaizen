import { ActivityDetailComponent } from './components/activity-detail/activity-detail.component';
import { ActivityRegisterComponent } from './components/activity-register/activity-register.component';
import { ActivityScheduleComponent } from './components/activity-schedule/activity-schedule.component';
import { AdminOrOfficeEmployeeGuard } from '@app/core/guards/admin-or-office-employee.guard';
import { AuthGuard } from '@app/core/guards/auth.guard';
import { NgModule } from '@angular/core';
import { OfficeEmployeeGuard } from '@app/core/guards/office-employee.guard';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
	{
		path: '',
		children: [
			{ path: '', component: ActivityScheduleComponent, canActivate: [ AuthGuard, AdminOrOfficeEmployeeGuard ] },
			{
				path: 'register',
				component: ActivityRegisterComponent,
				canActivate: [ AuthGuard, AdminOrOfficeEmployeeGuard ]
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
