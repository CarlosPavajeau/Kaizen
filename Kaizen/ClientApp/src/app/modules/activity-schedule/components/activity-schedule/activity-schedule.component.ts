import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivityScheduleDayComponent } from '@modules/activity-schedule/components/activity-schedule-day/activity-schedule-day.component';
import { ActivityScheduleMonthComponent } from '@modules/activity-schedule/components/activity-schedule-month/activity-schedule-month.component';
import { Activity } from '@modules/activity-schedule/models/activity';
import { ActivityScheduleView } from '@modules/activity-schedule/models/activity-schedule-view';
import { ActivityScheduleService } from '@modules/activity-schedule/services/activity-schedule.service';
import { ObservableStatus } from '@shared/models/observable-with-status';
import * as moment from 'moment';
import { Observable } from 'rxjs';
import { delay } from 'rxjs/operators';

@Component({
  selector: 'app-activity-schedule',
  templateUrl: './activity-schedule.component.html'
})
export class ActivityScheduleComponent implements OnInit {
  public ObsStatus: typeof ObservableStatus = ObservableStatus;
  public ScheduleView: typeof ActivityScheduleView = ActivityScheduleView;

  readonly activityScheduleViewNames: string[] = [ 'Mes', 'Semana', 'Día' ];
  readonly previousMessages: string[] = [ 'Mes anterior', 'Semana anterior', 'Día anterior' ];
  readonly nextMessages: string[] = [ 'Mes siguiente', 'Semana siguiente', 'Día siguiente' ];

  activities: Activity[] = [];
  activities$: Observable<Activity[]>;
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
    this.activities$ = this.activityScheduleService.getActivities().pipe(delay(3000));
    this.activities$.subscribe((activities) => {
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

  previousDate(): void {
    switch (this.view) {
      case ActivityScheduleView.Month: {
        this.scheduleMonth.previousMonth();
        break;
      }
      case ActivityScheduleView.Week: {
        break;
      }
      case ActivityScheduleView.Day: {
        this.scheduleDay.previousDay();
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
