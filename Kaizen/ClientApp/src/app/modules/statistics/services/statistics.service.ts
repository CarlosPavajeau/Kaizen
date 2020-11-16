import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { STATISTICS_API_URL } from '@global/endpoints';
import { DayStatistics, MontStatistics, YearStatistics } from '@modules/statistics/models/statistics';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class StatisticsService {
  constructor(private http: HttpClient) {}

  getCurrentYearStatistic(): Observable<YearStatistics> {
    return this.http.get<YearStatistics>(`${STATISTICS_API_URL}/CurrentYear`);
  }

  getCurrentMonthStatistics(): Observable<MontStatistics> {
    return this.http.get<MontStatistics>(`${STATISTICS_API_URL}/CurrentMonth`);
  }

  getCurrentDayStatistics(): Observable<DayStatistics> {
    return this.http.get<DayStatistics>(`${STATISTICS_API_URL}/CurrentDay`);
  }
}
