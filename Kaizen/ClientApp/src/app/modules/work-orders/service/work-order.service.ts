import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { WORK_ORDERS_API_URL } from '@global/endpoints';
import { Sector } from '@modules/work-orders/models/sector';
import { WorkOrder } from '@modules/work-orders/models/work-order';
import { Observable } from 'rxjs';

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
