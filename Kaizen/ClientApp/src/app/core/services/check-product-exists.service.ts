import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CheckEntityExistsService } from '@core/services/check-entity-exists.service';
import { PRODUCTS_API_URL } from '@global/endpoints';

@Injectable({
  providedIn: 'root'
})
export class CheckProductExistsService extends CheckEntityExistsService {
  constructor(http: HttpClient) {
    super(http, PRODUCTS_API_URL);
  }
}
