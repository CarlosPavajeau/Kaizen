import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CheckEntityExistsService } from '@core/services/check-entity-exists.service';
import { AUTH_API_URL } from '@global/endpoints';

@Injectable({
  providedIn: 'root'
})
export class CheckUserExistsService extends CheckEntityExistsService {
  constructor(http: HttpClient) {
    super(http, AUTH_API_URL);
  }
}
