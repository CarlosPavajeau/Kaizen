import { Component, OnDestroy, OnInit } from '@angular/core';
import { Client } from '@modules/clients/models/client';
import { ClientState } from '@modules/clients/models/client-state';
import { ClientService } from '@modules/clients/services/client.service';
import { NewClientSignalrService } from '@modules/clients/services/new-client-signalr.service';
import { ObservableStatus } from '@shared/models/observable-with-status';
import { NotificationsService } from '@shared/services/notifications.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-client-requests',
  templateUrl: './client-requests.component.html',
  styleUrls: [ './client-requests.component.scss' ]
})
export class ClientRequestsComponent implements OnInit, OnDestroy {
  public ObsStatus: typeof ObservableStatus = ObservableStatus;

  clientRequests: Client[];
  clientRequests$: Observable<Client[]>;
  updatingClientRequest = false;

  constructor(
    private clientService: ClientService,
    private notificationsService: NotificationsService,
    private newClientSignalr: NewClientSignalrService
  ) {}

  ngOnInit(): void {
    this.loadClientRequests();

    this.newClientSignalr.signalReceived.subscribe((newClient: Client) => {
      this.clientRequests.push(newClient);
      this.notificationsService.addMessage(`Se ha registrado un nuevo cliente`, 'Ok', 'left');
    });
  }

  ngOnDestroy(): void {
    this.newClientSignalr.signalReceived.unsubscribe();
  }

  private loadClientRequests(): void {
    this.clientService.getClientRequests().subscribe((clientRequests) => {
      this.clientRequests = clientRequests;
    });
    this.clientRequests$ = this.clientService.getClientRequests();
  }

  acceptClient(client: Client): void {
    client.state = ClientState.Accepted;
    this.processClient(client);
  }

  rejectClient(client: Client): void {
    client.state = ClientState.Rejected;
    this.processClient(client);
  }

  private processClient(client: Client): void {
    this.updatingClientRequest = true;
    this.clientService.updateClient(client).subscribe((clientUpdate) => {
      this.clientRequests = this.clientRequests.filter((c) => c.id !== clientUpdate.id);
      this.updatingClientRequest = false;
    });
  }
}
