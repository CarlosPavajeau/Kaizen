import { Injectable } from '@angular/core';
import { AuthenticationService } from '@core/authentication/authentication.service';
import { BaseSignalrService } from '@core/services/base-signalr.service';
import { Client } from '@modules/clients/models/client';

@Injectable({
  providedIn: 'root'
})
export class NewClientSignalrService extends BaseSignalrService<Client> {
  constructor(authService: AuthenticationService) {
    super(authService, '/ClientsHub', 'NewClient');
  }
}
