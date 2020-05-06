import { Injectable } from '@angular/core';
import { CLIENTS_API_URL } from '@global/endpoints';
import { CheckEntityExistsService } from '@core/services/check-entity-exists.service';
import { HttpClient } from '@angular/common/http';

@Injectable({
	providedIn: 'root'
})
export class CheckClientExistsService extends CheckEntityExistsService {
	constructor(http: HttpClient) {
		super(http, CLIENTS_API_URL);
	}
}
