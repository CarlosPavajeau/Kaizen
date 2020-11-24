import { Component, Input, OnInit } from '@angular/core';
import { Activity } from '@modules/activity-schedule/models/activity';
import { Hour } from '@modules/activity-schedule/models/hour';
import { Moment } from 'moment';

@Component({
  selector: 'app-activity-schedule-day',
  templateUrl: './activity-schedule-day.component.html',
  styleUrls: [ './activity-schedule-day.component.scss' ]
})
export class ActivityScheduleDayComponent implements OnInit {
  @Input() selectedDate: Moment;
  @Input() activities: Activity[];

  hours: Hour[] = [];
  start: Moment;

  constructor() {}

  ngOnInit(): void {
    this.buildDay();
  }

  private buildDay(): void {
    this.hours = [];
    this.start = this.selectedDate.clone();
    this.start.hour(0).minute(0).second(0).millisecond(0);

    for (let i = 0; i < 24; ++i) {
      this.hours.push({
        name: this.start.format('hh'),
        number: this.start.hour(),
        date: this.start
      });
      this.start = this.start.clone();
      this.start.add(1, 'h');
    }

    this.hours.forEach((hour) => {
      hour.activities = this.loadActivitiesForHour(hour.date);
    });
  }

  private loadActivitiesForHour(date: Moment) {
    return this.activities.filter((activity) => {
      return (
        activity.date.getMonth() === date.month() &&
        activity.date.getDate() === date.date() &&
        activity.date.getHours() === date.hour()
      );
    });
  }

  nextDay(): void {
    this.changeDay(+1);
  }

  previusDay(): void {
    this.changeDay(-1);
  }

  private changeDay(to: number): void {
    this.selectedDate.date(this.selectedDate.date() + to);
    this.buildDay();
  }

  showCurrentDay(currentDate: Moment): void {
    this.selectedDate = currentDate;
    this.buildDay();
  }
}
