import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CLIENTS_API_URL } from '@global/endpoints';

@Injectable({
	providedIn: 'root'
})
export class CheckClientExistsService {
	constructor(private http: HttpClient) {}

	checkClientExists(id: string): Observable<boolean> {
		return this.http.get<boolean>(`${CLIENTS_API_URL}/CheckClientExists/${id}`);
	}
}
