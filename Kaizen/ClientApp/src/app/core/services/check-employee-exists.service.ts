import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { EMPLOYEES_API_URL } from '@global/endpoints';
import { CheckEntityExistsService } from '@core/services/check-entity-exists.service';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class CheckEmployeeExistsService extends CheckEntityExistsService {
  constructor(http: HttpClient) {
    super(http, EMPLOYEES_API_URL);
  }
}
