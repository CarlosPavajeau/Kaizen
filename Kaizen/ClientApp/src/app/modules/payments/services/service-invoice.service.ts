import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ServiceInvoice } from '../models/service-invoice';
import { Observable } from 'rxjs';
import { SERVICE_INVOICES_API_URL } from '@global/endpoints';

@Injectable({
  providedIn: 'root'
})
export class ServiceInvoiceService {
  constructor(private http: HttpClient) {}

  getServiceInvoices(): Observable<ServiceInvoice[]> {
    return this.http.get<ServiceInvoice[]>(`${SERVICE_INVOICES_API_URL}`);
  }

  getClientServiceInvoices(clientId: string): Observable<ServiceInvoice[]> {
    return this.http.get<ServiceInvoice[]>(`${SERVICE_INVOICES_API_URL}/ClientInvoices/${clientId}`);
  }

  getServiceInvoice(id: number): Observable<ServiceInvoice> {
    return this.http.get<ServiceInvoice>(`${SERVICE_INVOICES_API_URL}/${id}`);
  }

  updateServiceInvoice(serviceInvoice: ServiceInvoice): Observable<ServiceInvoice> {
    return this.http.put<ServiceInvoice>(`${SERVICE_INVOICES_API_URL}/${serviceInvoice.id}`, serviceInvoice);
  }
}
