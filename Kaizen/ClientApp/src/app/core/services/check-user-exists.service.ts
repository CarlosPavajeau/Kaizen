import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Endpoints } from '@global/endpoints';

@Injectable({
	providedIn: 'root'
})
export class CheckUserExistsService {
	constructor(private http: HttpClient) {}

	checkUserExists(username: string): Observable<boolean> {
		return this.http.get<boolean>(`${Endpoints.AuthUrl}/CheckUserExists/${username}`);
	}
}
