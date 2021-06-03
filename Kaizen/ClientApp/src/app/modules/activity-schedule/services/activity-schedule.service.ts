import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { buildIsoDate } from '@base/app/core/utils/date-utils';
import { ACTIVITIES_API_URL } from '@global/endpoints';
import { Activity } from '@modules/activity-schedule/models/activity';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ActivityScheduleService {
  constructor(private http: HttpClient) {
  }

  getActivities(): Observable<Activity[]> {
    return this.http.get<Activity[]>(ACTIVITIES_API_URL);
  }

  getActivitiesByYearAndMonth(year: number, month: number): Observable<Activity[]> {
    return this.http.get<Activity[]>(`${ ACTIVITIES_API_URL }/${ year }/${ month }`);
  }

  getActivity(code: number): Observable<Activity> {
    return this.http.get<Activity>(`${ ACTIVITIES_API_URL }/${ code }`);
  }

  getPendingEmployeeActivities(employeeId: string): Observable<Activity[]> {
    const today = new Date();
    return this.http.get<Activity[]>(
      `${ ACTIVITIES_API_URL }/EmployeeActivities/${ employeeId }?date=${ buildIsoDate(today, '00:00').toISOString() }`,
    );
  }

  getPendingClientActivities(clientId: string): Observable<Activity[]> {
    return this.http.get<Activity[]>(`${ ACTIVITIES_API_URL }/ClientActivities/${ clientId }`);
  }

  getAppliedClientActivities(clientId: string): Observable<Activity[]> {
    return this.http.get<Activity[]>(`${ ACTIVITIES_API_URL }/AppliedClientActivities/${ clientId }`);
  }

  saveActivity(activity: Activity): Observable<Activity> {
    return this.http.post<Activity>(ACTIVITIES_API_URL, activity);
  }

  updateActivity(activity: Activity): Observable<Activity> {
    return this.http.put<Activity>(`${ ACTIVITIES_API_URL }/${ activity.code }`, activity);
  }
}
