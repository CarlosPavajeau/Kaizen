import * as moment from 'moment';
import { Activity } from '@modules/activity-schedule/models/activity';
import { ActivityScheduleDayComponent } from '@modules/activity-schedule/components/activity-schedule-day/activity-schedule-day.component';
import { ActivityScheduleMonthComponent } from '@modules/activity-schedule/components/activity-schedule-month/activity-schedule-month.component';
import { ActivityScheduleService } from '@modules/activity-schedule/services/activity-schedule.service';
import { ActivityScheduleView } from '@modules/activity-schedule/models/activity-schedule-view';
import { Component, OnInit, ViewChild } from '@angular/core';

@Component({
	selector: 'app-activity-schedule',
	templateUrl: './activity-schedule.component.html',
	styleUrls: [ './activity-schedule.component.css' ]
})
export class ActivityScheduleComponent implements OnInit {
	readonly activityScheduleViewNames: string[] = [ 'Mes', 'Semana', 'Día' ];
	readonly previusMessages: string[] = [ 'Mes anterior', 'Semana anterior', 'Día anterior' ];
	readonly nextMessages: string[] = [ 'Mes siguiente', 'Semana siguiente', 'Día siguiente' ];

	activities: Activity[] = [];
	currentDate = moment();
	selectedDate: moment.Moment = this.currentDate.clone();
	view: ActivityScheduleView = ActivityScheduleView.Month;

	@ViewChild(ActivityScheduleDayComponent) scheduleDay: ActivityScheduleDayComponent;
	@ViewChild(ActivityScheduleMonthComponent) scheduleMonth: ActivityScheduleMonthComponent;

	constructor(private activityScheduleService: ActivityScheduleService) {}

	ngOnInit(): void {
		this.loadData();
	}

	private loadData(): void {
		this.activityScheduleService.getActivities().subscribe((activities) => {
			this.activities = activities;
			this.onLoadData();
		});
	}

	private onLoadData(): void {
		this.activities.forEach((activity) => (activity.date = new Date(activity.date)));

		if (this.scheduleDay) {
			this.scheduleDay.activities = this.activities;
		}
		if (this.scheduleMonth) {
			this.scheduleMonth.activities = this.activities;
		}

		this.showCurrentDate();
	}

	nextDate(): void {
		switch (this.view) {
			case ActivityScheduleView.Month: {
				this.scheduleMonth.nextMonth();
				break;
			}
			case ActivityScheduleView.Week: {
				break;
			}
			case ActivityScheduleView.Day: {
				this.scheduleDay.nextDay();
				break;
			}
		}
	}

	previusDate(): void {
		switch (this.view) {
			case ActivityScheduleView.Month: {
				this.scheduleMonth.previusMonth();
				break;
			}
			case ActivityScheduleView.Week: {
				break;
			}
			case ActivityScheduleView.Day: {
				this.scheduleDay.previusDay();
				break;
			}
		}
	}

	showCurrentDate(): void {
		this.selectedDate = this.currentDate.clone();

		switch (this.view) {
			case ActivityScheduleView.Month: {
				this.scheduleMonth.showCurrentMonth(this.currentDate);
				break;
			}
			case ActivityScheduleView.Week: {
				break;
			}
			case ActivityScheduleView.Day: {
				this.scheduleDay.showCurrentDay(this.currentDate);
				break;
			}
		}
	}

	setView(view: ActivityScheduleView | number): void {
		this.view = view;
	}

	onSelectDay(date: moment.Moment): void {
		this.selectedDate = date;
		this.setView(ActivityScheduleView.Day);
	}
}
