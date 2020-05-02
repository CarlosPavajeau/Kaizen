import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Endpoints } from '@app/global/endpoints';

@Injectable({
	providedIn: 'root'
})
export class CheckEmployeeExistsService {
	constructor(private http: HttpClient) {}

	checkEmployeeExists(id: string): Observable<boolean> {
		return this.http.get<boolean>(`${Endpoints.EmployeesUrl}/CheckEmployeeExists/${id}`);
	}
}
