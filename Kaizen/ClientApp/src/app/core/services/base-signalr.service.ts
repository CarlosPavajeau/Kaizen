import { EventEmitter, Injectable, OnDestroy, OnInit, Output } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';
import { AuthenticationService } from '@core/authentication/authentication.service';

@Injectable({
  providedIn: 'root'
})
export class BaseSignalrService implements OnDestroy {
  protected hubConnection: HubConnection;

  constructor() {
  }

  protected buildConnection(hubUrl: string, token?: string): void {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(hubUrl, { accessTokenFactory: () => token })
      .build();
  }

  protected startConnection(): void {
    this.hubConnection
      .start()
      .then(() => {
        console.log('Connection started');
      })
      .catch((err) => {
        console.log('Error: ' + err);
        setTimeout(() => this.startConnection(), 5000);
      });
  }

  ngOnDestroy(): void {
    this.hubConnection.stop().then(() => {
      console.log('Connection stopped');
    });
  }
}
