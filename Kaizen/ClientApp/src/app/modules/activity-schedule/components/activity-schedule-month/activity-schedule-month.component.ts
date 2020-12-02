import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Activity } from '@modules/activity-schedule/models/activity';
import { Day } from '@modules/activity-schedule/models/day';
import { Week } from '@modules/activity-schedule/models/week';
import { Moment } from 'moment';

@Component({
  selector: 'app-activity-schedule-month',
  templateUrl: './activity-schedule-month.component.html',
  styleUrls: [ './activity-schedule-month.component.scss' ]
})
export class ActivityScheduleMonthComponent implements OnInit {
  @Input() selectedDate: Moment;
  @Input() activities: Activity[] = [];

  @Output() selectDay = new EventEmitter<Moment>();

  start: Moment;
  weeks: Week[] = [];

  constructor() {}

  ngOnInit(): void {
    this.buildMonth();
  }

  private buildMonth(): void {
    this.start = this.selectedDate.clone();
    this.start.date(1);
    this.resetTime(this.start);

    this.weeks = [];
    let done = false;
    const date = this.start.clone();
    let monthIndex = date.month(),
      count = 0;

    while (!done) {
      this.weeks.push({
        days: this.buildWeek(date.clone(), this.selectedDate)
      });

      date.add(1, 'w');
      done = count++ > 2 && monthIndex !== date.month();
      monthIndex = date.month();
    }

    this.loadActivitiesForDay();
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

  private resetTime(time: moment.Moment) {
    time.day(0).day(0).hour(0).minute(0).second(0).millisecond(0);
  }

  private buildWeek(date: moment.Moment, month: moment.Moment) {
    const days: Day[] = [];

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
