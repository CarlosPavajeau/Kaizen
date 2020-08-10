import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Sector } from '@modules/work-orders/models/sector';
import { WORK_ORDERS_API_URL } from '@global/endpoints';
import { WorkOrder } from '@modules/work-orders/models/work-order';

@Injectable({
  providedIn: 'root'
})
export class WorkOrderService {
  constructor(private http: HttpClient) {}

  getWorkOrders(): Observable<WorkOrder[]> {
    return this.http.get<WorkOrder[]>(WORK_ORDERS_API_URL);
  }

  getWorkOrder(code: number): Observable<WorkOrder> {
    return this.http.get<WorkOrder>(`${WORK_ORDERS_API_URL}/${code}`);
  }

  getWorkOrderOfActivity(activityCode: number): Observable<WorkOrder> {
    return this.http.get<WorkOrder>(`${WORK_ORDERS_API_URL}/ActivityWorkOrder/${activityCode}`);
  }

  getSectors(): Observable<Sector[]> {
    return this.http.get<Sector[]>(`${WORK_ORDERS_API_URL}/Sectors`);
  }

  saveWorkOrder(workOrder: WorkOrder): Observable<WorkOrder> {
    return this.http.post<WorkOrder>(WORK_ORDERS_API_URL, workOrder);
  }

  updateWorkOrder(workOrder: WorkOrder): Observable<WorkOrder> {
    return this.http.put<WorkOrder>(`${WORK_ORDERS_API_URL}/${workOrder.code}`, workOrder);
  }
}
