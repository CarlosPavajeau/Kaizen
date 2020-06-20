import { AuthenticationService } from '@core/authentication/authentication.service';
import { BaseSignalrService } from '@app/core/services/base-signalr.service';
import { Client } from '../models/client';
import { Injectable } from '@angular/core';

@Injectable({
	providedIn: 'root'
})
export class NewClientSignalrService extends BaseSignalrService<Client> {
	constructor(authService: AuthenticationService) {
		super(authService, '/ClientsHub', 'NewClient');
	}
}
