import { Component, OnInit } from '@angular/core';
import { StatisticsService } from '@modules/statistics/services/statistics.service';
import { DayStatistics } from '../../models/statistics';

@Component({
  selector: 'app-today-statistics',
  templateUrl: './today-statistics.component.html',
  styleUrls: [ './today-statistics.component.scss' ]
})
export class TodayStatisticsComponent implements OnInit {
  dayStatistics: DayStatistics;

  constructor(private statisticsService: StatisticsService) {}

  ngOnInit(): void {
    this.loadData();
  }

  private loadData(): void {
    this.statisticsService.getCurrentDayStatistics().subscribe((dayStatistics: DayStatistics) => {
      this.dayStatistics = dayStatistics;
    });
  }
}
