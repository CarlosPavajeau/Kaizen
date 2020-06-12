import { ACTIVITIES_API_URL } from '@global/endpoints';
import { Activity } from '@modules/activity-schedule/models/activity';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
	providedIn: 'root'
})
export class ActivityScheduleService {
	constructor(private http: HttpClient) {}

	getActivities(): Observable<Activity[]> {
		return this.http.get<Activity[]>(ACTIVITIES_API_URL);
	}

	getActivity(code: number): Observable<Activity> {
		return this.http.get<Activity>(`${ACTIVITIES_API_URL}/${code}`);
	}

	saveActivity(activity: Activity): Observable<Activity> {
		return this.http.post<Activity>(ACTIVITIES_API_URL, activity);
	}

	updateActivity(activity: Activity): Observable<Activity> {
		return this.http.put<Activity>(`${ACTIVITIES_API_URL}/${activity.code}`, activity);
	}
}
