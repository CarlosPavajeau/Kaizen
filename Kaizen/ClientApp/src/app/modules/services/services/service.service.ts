import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SERVICES_API_URL } from '@global/endpoints';
import { Service } from '@modules/services/models/service';
import { ServiceType } from '@modules/services/models/service-type';
import { Observable } from 'rxjs';

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

  getServiceTypes(): Observable<ServiceType[]> {
    return this.http.get<ServiceType[]>(`${SERVICES_API_URL}/ServiceTypes`);
  }

  saveService(service: Service): Observable<Service> {
    return this.http.post<Service>(SERVICES_API_URL, service);
  }

  saveServiceType(serviceType: ServiceType): Observable<ServiceType> {
    return this.http.post<ServiceType>(`${SERVICES_API_URL}/ServiceTypes`, serviceType);
  }

  updateService(service: Service): Observable<Service> {
    return this.http.put<Service>(`${SERVICES_API_URL}/${service.code}`, service);
  }
}
