import { EventEmitter, Injectable } from '@angular/core';
import { AuthenticationService } from '@core/authentication/authentication.service';
import { BaseSignalrService } from '@core/services/base-signalr.service';
import { Client } from '@modules/clients/models/client';

@Injectable({
  providedIn: 'root'
})
export class ClientSignalrService extends BaseSignalrService {
  onNewClientRegister: EventEmitter<Client> = new EventEmitter<Client>();

  constructor(private authService: AuthenticationService) {
    super();
    this.startConnection();
    this.addOnNewClientRegister();
  }

  public startConnection() {
    super.buildConnection('/ClientsHub', this.authService.getToken());
    super.startConnection();
  }

  public addOnNewClientRegister(): void {
    this.hubConnection.on('NewClient', (client: Client) => {
      this.onNewClientRegister.emit(client);
    });
  }
}
