import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AUTH_API_URL } from '@global/endpoints';
import { CheckEntityExistsService } from '@core/services/check-entity-exists.service';
import { HttpClient } from '@angular/common/http';

@Injectable({
	providedIn: 'root'
})
export class CheckUserExistsService extends CheckEntityExistsService {
	constructor(http: HttpClient) {
		super(http, AUTH_API_URL);
	}
}
