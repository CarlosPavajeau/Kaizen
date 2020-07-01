import { Component, OnInit, AfterViewInit } from '@angular/core';
import { ActivityScheduleService } from '../../services/activity-schedule.service';
import { Activity } from '../../models/activity';
import * as moment from 'moment';
import { Week } from '../../models/week';
import { Day } from '../../models/day';

@Component({
	selector: 'app-activity-schedule',
	templateUrl: './activity-schedule.component.html',
	styleUrls: [ './activity-schedule.component.css' ]
})
export class ActivityScheduleComponent implements OnInit {
	activities: Activity[] = [];
	weeks: Week[];
	currentMonth = moment();
	selectedMonth: moment.Moment;

	constructor(private activityScheduleService: ActivityScheduleService) {}

	ngOnInit(): void {
		this.showCurrentMonth();
		this.loadData();
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

	private buildMonth(start: moment.Moment, month: moment.Moment): void {
		this.resetTime(start);

		this.weeks = [];
		let done = false,
			date = start.clone(),
			monthIndex = date.month(),
			count = 0;

		while (!done) {
			this.weeks.push({
				days: this.buildWeek(date.clone(), this.selectedMonth)
			});

			date.add(1, 'w');
			done = count++ > 2 && monthIndex !== date.month();
			monthIndex = date.month();
		}
	}

	private resetTime(time: moment.Moment) {
		time.day(0).day(0).hour(0).minute(0).second(0).millisecond(0);
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

	nextMonth(): void {
		this.changeSelectedMonth(+1);
	}

	previusMonth(): void {
		this.changeSelectedMonth(-1);
	}

	private changeSelectedMonth(to: number): void {
		const newMonth = this.selectedMonth.clone();
		this.resetTime(newMonth.month(newMonth.month() + to).date(1));
		this.selectedMonth.month(this.selectedMonth.month() + to);
		this.buildMonth(newMonth, this.selectedMonth);
		this.loadActivitiesForDay();
	}

	showCurrentMonth(): void {
		const start = this.currentMonth.clone();
		start.date(1);
		this.selectedMonth = this.currentMonth.clone();
		this.buildMonth(start, this.currentMonth);

		if (this.activities) {
			this.loadActivitiesForDay();
		}
	}
}
