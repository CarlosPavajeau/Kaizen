import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CheckEntityExistsService } from '@core/services/check-entity-exists.service';
import { EQUIPMENTS_API_URL } from '@global/endpoints';

@Injectable({
  providedIn: 'root'
})
export class CheckEquipmentExistsService extends CheckEntityExistsService {
  constructor(http: HttpClient) {
    super(http, EQUIPMENTS_API_URL);
  }
}
