import { Component, OnInit, AfterViewInit } from '@angular/core';
import { ActivityScheduleService } from '../../services/activity-schedule.service';
import { Activity } from '../../models/activity';
import * as moment from 'moment';
import { Week } from '../../models/week';
import { Day } from '../../models/day';
import { DatePipe } from '@angular/common';

@Component({
	selector: 'app-activity-schedule',
	templateUrl: './activity-schedule.component.html',
	styleUrls: [ './activity-schedule.component.css' ]
})
export class ActivityScheduleComponent implements OnInit {
	activities: Activity[] = [];
	currentMonth: Date = new Date();
	weeks: Week[];
	activitiesForDate: Map<moment.Moment, Activity[]>;
	selected = moment();

	constructor(private activityScheduleService: ActivityScheduleService) {}

	ngOnInit(): void {
		this.loadData();
		this.buildMonth();
	}

	private loadData(): void {
		this.activityScheduleService.getActivities().subscribe((activities) => {
			this.activities = activities;
			this.activities.forEach((activity) => (activity.date = new Date(activity.date)));
			this.loadActivitiesForDay();
		});
	}

	private loadActivitiesForDay(): void {
		this.weeks.forEach((week) => {
			week.days.forEach((day) => {
				day.activities = this.activitiesInDate(day.date);
			});
		});
	}

	private activitiesInDate(date: moment.Moment): Activity[] {
		return this.activities.filter((activity) => {
			return activity.date.getMonth() == date.month() && activity.date.getDate() == date.date();
		});
	}

	private buildMonth(): void {
		let month = this.selected.clone();
		let start = this.selected.clone();
		start.date(1);
		start.day(0).day(0).hour(0).minute(0).second(0).millisecond(0);

		this.weeks = [];
		let done = false,
			date = start.clone(),
			monthIndex = date.month(),
			count = 0;

		while (!done) {
			this.weeks.push({
				days: this.buildWeek(date.clone(), month)
			});
			date.add(1, 'w');
			done = count++ > 2 && monthIndex !== date.month();
			monthIndex = date.month();
		}
	}

	private buildWeek(date: moment.Moment, month: moment.Moment) {
		let days: Day[] = [];
		for (let i = 0; i < 7; ++i) {
			days.push({
				name: date.format('dd').substring(0, 1),
				number: date.date(),
				isCurrentMonth: date.month() === month.month(),
				isToday: date.isSame(new Date(), 'day'),
				date: date
			});
			date = date.clone();
			date.add(1, 'd');
		}

		return days;
	}

	buildTooltipMessage(activity: Activity): string {
		let datePipe: DatePipe = new DatePipe('en-US');
		return `Actividad N° ${activity.code}, a las ${datePipe.transform(
			activity.date,
			'h:mm a'
		)}. Para el cliente ${activity.client.lastName} ${activity.client.firstName}`;
	}
}
