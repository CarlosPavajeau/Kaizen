import { Component, OnInit } from '@angular/core';
import { DayStatistics } from '@modules/statistics/models/statistics';
import { StatisticsService } from '@modules/statistics/services/statistics.service';
import { ObservableStatus } from '@shared/models/observable-with-status';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-today-statistics',
  templateUrl: './today-statistics.component.html',
  styleUrls: [ './today-statistics.component.scss' ]
})
export class TodayStatisticsComponent implements OnInit {
  public ObsStatus: typeof ObservableStatus = ObservableStatus;

  dayStatistics$: Observable<DayStatistics>;

  constructor(private statisticsService: StatisticsService) {}

  ngOnInit(): void {
    this.loadData();
  }

  private loadData(): void {
    this.dayStatistics$ = this.statisticsService.getCurrentDayStatistics();
  }
}
