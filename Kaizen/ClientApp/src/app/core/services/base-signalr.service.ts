import { Injectable, OnDestroy } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';

@Injectable({
  providedIn: 'root'
})
export class BaseSignalrService {
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
      });
  }
}
