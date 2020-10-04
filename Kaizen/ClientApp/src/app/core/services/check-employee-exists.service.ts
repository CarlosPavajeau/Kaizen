import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CheckEntityExistsService } from '@core/services/check-entity-exists.service';
import { EMPLOYEES_API_URL } from '@global/endpoints';

@Injectable({
  providedIn: 'root'
})
export class CheckEmployeeExistsService extends CheckEntityExistsService {
  constructor(http: HttpClient) {
    super(http, EMPLOYEES_API_URL);
  }
}
