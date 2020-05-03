import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { SERVICES_API_URL } from '@global/endpoints';
import { Service } from '@modules/services/models/service';

@Injectable({
	providedIn: 'root'
})
export class ServiceService {
	constructor(private http: HttpClient) {}

	getServices(): Observable<Service[]> {
		return this.http.get<Service[]>(SERVICES_API_URL);
	}

	getService(id: string): Observable<Service> {
		return this.http.get<Service>(`${SERVICES_API_URL}/${id}`);
	}

	saveService(service: Service): Observable<Service> {
		return this.http.post<Service>(SERVICES_API_URL, service);
	}

	updateService(service: Service): Observable<Service> {
		return this.http.put<Service>(`${SERVICES_API_URL}/${service.code}`, service);
	}
}
