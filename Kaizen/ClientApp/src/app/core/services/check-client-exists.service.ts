import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CheckEntityExistsService } from '@core/services/check-entity-exists.service';
import { CLIENTS_API_URL } from '@global/endpoints';

@Injectable({
  providedIn: 'root'
})
export class CheckClientExistsService extends CheckEntityExistsService {
  constructor(http: HttpClient) {
    super(http, CLIENTS_API_URL);
  }
}
