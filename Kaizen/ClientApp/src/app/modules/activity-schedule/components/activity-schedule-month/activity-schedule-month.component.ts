import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Activity } from '@modules/activity-schedule/models/activity';
import { Day } from '@modules/activity-schedule/models/day';
import { Week } from '@modules/activity-schedule/models/week';
import { Moment } from 'moment';
import { ActivityScheduleService } from '@modules/activity-schedule/services/activity-schedule.service';

@Component({
  selector: 'app-activity-schedule-month',
  templateUrl: './activity-schedule-month.component.html',
  styleUrls: [ './activity-schedule-month.component.scss' ],
})
export class ActivityScheduleMonthComponent implements OnInit {
  @Input() selectedDate: Moment;
  activities: Activity[] = [];

  @Output() selectDay = new EventEmitter<Moment>();
  @Output() onLoadActivities = new EventEmitter();

  start: Moment;
  weeks: Week[] = [];

  constructor(private activityScheduleService: ActivityScheduleService) {
  }

  ngOnInit(): void {
    this.buildMonth();
  }

  private buildMonth(): void {
    this.start = this.selectedDate.clone();
    this.start.date(1);
    ActivityScheduleMonthComponent.resetTime(this.start);

    this.weeks = [];
    let done = false;
    const date = this.start.clone();
    let monthIndex = date.month(),
      count = 0;

    while (!done) {
      this.weeks.push({
        days: ActivityScheduleMonthComponent.buildWeek(date.clone(), this.selectedDate),
      });

      date.add(1, 'w');
      done = count++ > 2 && monthIndex !== date.month();
      monthIndex = date.month();
    }

    this.activityScheduleService.getActivitiesByYearAndMonth(this.selectedDate.year(), this.selectedDate.month() + 1)
      .subscribe((activities: Activity[]) => {
        this.activities = activities;
        this.activities.forEach((activity) => (activity.date = new Date(activity.date)));
        this.loadActivitiesForDay();
        this.onLoadActivities.emit();
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
      return activity.date.getMonth() === date.month() && activity.date.getDate() === date.date();
    });
  }

  private static resetTime(time: moment.Moment) {
    time.day(0).day(0).hour(0).minute(0).second(0).millisecond(0);
  }

  private static buildWeek(date: moment.Moment, month: moment.Moment) {
    const days: Day[] = [];

    for (let i = 0; i < 7; ++i) {
      days.push({
        name: date.format('dd').substring(0, 1),
        number: date.date(),
        isCurrentMonth: date.month() === month.month(),
        isToday: date.isSame(new Date(), 'day'),
        date: date,
      });

      date = date.clone();
      date.add(1, 'd');
    }

    return days;
  }

  nextMonth(): void {
    this.changeSelectedMonth(+1);
  }

  previousMonth(): void {
    this.changeSelectedMonth(-1);
  }

  private changeSelectedMonth(to: number): void {
    this.selectedDate.month(this.selectedDate.month() + to);
    this.buildMonth();
  }

  showCurrentMonth(currentDate: Moment): void {
    this.selectedDate = currentDate;
    this.buildMonth();
  }

  onSelectDay(day: Moment): void {
    this.selectDay.emit(day);
  }
}
