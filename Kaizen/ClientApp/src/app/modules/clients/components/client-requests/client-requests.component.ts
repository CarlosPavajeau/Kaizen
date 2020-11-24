import { Component, OnInit } from '@angular/core';
import { Client } from '@modules/clients/models/client';
import { ClientState } from '@modules/clients/models/client-state';
import { ClientService } from '@modules/clients/services/client.service';
import { NewClientSignalrService } from '@modules/clients/services/new-client-signalr.service';
import { NotificationsService } from '@shared/services/notifications.service';

@Component({
  selector: 'app-client-requests',
  templateUrl: './client-requests.component.html',
  styleUrls: [ './client-requests.component.scss' ]
})
export class ClientRequestsComponent implements OnInit {
  clientRequests: Client[];
  updatingClientResquest = false;

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

  private loadClientRequests(): void {
    this.clientService.getClientRequests().subscribe((clientRequests) => {
      this.clientRequests = clientRequests;
    });
  }

  acceptClient(client: Client): void {
    client.state = ClientState.Accepted;
    this.proccessClient(client);
  }

  rejectClient(client: Client): void {
    client.state = ClientState.Rejected;
    this.proccessClient(client);
  }

  private proccessClient(client: Client): void {
    this.updatingClientResquest = true;
    this.clientService.updateClient(client).subscribe((clientUpdate) => {
      this.clientRequests = this.clientRequests.filter((c) => c.id !== clientUpdate.id);
      this.updatingClientResquest = false;
    });
  }
}
