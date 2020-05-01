import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Equipment } from '@modules/inventory/equipments/models/equipment';
import { Endpoints } from '@global/endpoints';

@Injectable({
	providedIn: 'root'
})
export class EquipmentService {
	constructor(private http: HttpClient) {}

	getEquipments(): Observable<Equipment[]> {
		return this.http.get<Equipment[]>(Endpoints.EquipmentsUrl);
	}

	getEquipment(code: string): Observable<Equipment> {
		return this.http.get<Equipment>(`${Endpoints.EmployeesUrl}/${code}`);
	}

	saveEquipment(equipment: Equipment): Observable<Equipment> {
		return this.http.post<Equipment>(Endpoints.EquipmentsUrl, equipment);
	}

	updateEquipment(equipment: Equipment): Observable<Equipment> {
		return this.http.put<Equipment>(`${Endpoints.EquipmentsUrl}/${equipment.code}`, equipment);
	}
}
