import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SERVICE_INVOICES_API_URL } from '@global/endpoints';
import { ServiceInvoice } from '@modules/payments/models/service-invoice';
import { Observable } from 'rxjs';
import { PayModel } from '../models/pay';

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

  payServiceInvoice(serviceInvoice: ServiceInvoice, paymentModel: PayModel): Observable<ServiceInvoice> {
    return this.http.post<ServiceInvoice>(`${SERVICE_INVOICES_API_URL}/Pay/${serviceInvoice.id}`, paymentModel);
  }
}
