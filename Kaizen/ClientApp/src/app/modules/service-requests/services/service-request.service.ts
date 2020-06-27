import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { SERVICE_REQUESTS_API_URL } from '@global/endpoints';
import { ServiceRequest } from '@modules/service-requests/models/service-request';
import { Observable } from 'rxjs';

@Injectable({
	providedIn: 'root'
})
export class ServiceRequestService {
	constructor(private http: HttpClient) {}

	getServiceRequests(): Observable<ServiceRequest[]> {
		return this.http.get<ServiceRequest[]>(SERVICE_REQUESTS_API_URL);
	}

	getServiceRequest(id: number): Observable<ServiceRequest> {
		return this.http.get<ServiceRequest>(`${SERVICE_REQUESTS_API_URL}/${id}`);
	}

	getPendingServiceRequest(clientId: string): Observable<ServiceRequest> {
		return this.http.get<ServiceRequest>(`${SERVICE_REQUESTS_API_URL}/PendingServiceRequest/${clientId}`);
	}

	saveServiceRequest(serviceRequest: ServiceRequest): Observable<ServiceRequest> {
		return this.http.post<ServiceRequest>(SERVICE_REQUESTS_API_URL, serviceRequest);
	}

	updateServiceRequest(serviceRequest: ServiceRequest): Observable<ServiceRequest> {
		return this.http.put<ServiceRequest>(`${SERVICE_REQUESTS_API_URL}/${serviceRequest.code}`, serviceRequest);
	}
}
