import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { EQUIPMENTS_API_URL } from '@global/endpoints';
import { Equipment } from '@modules/inventory/equipments/models/equipment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EquipmentService {
  constructor(private http: HttpClient) {}

  getEquipments(): Observable<Equipment[]> {
    return this.http.get<Equipment[]>(EQUIPMENTS_API_URL);
  }

  getEquipment(code: string): Observable<Equipment> {
    return this.http.get<Equipment>(`${EQUIPMENTS_API_URL}/${code}`);
  }

  saveEquipment(equipment: Equipment): Observable<Equipment> {
    return this.http.post<Equipment>(EQUIPMENTS_API_URL, equipment);
  }

  updateEquipment(equipment: Equipment): Observable<Equipment> {
    return this.http.put<Equipment>(`${EQUIPMENTS_API_URL}/${equipment.code}`, equipment);
  }
}
