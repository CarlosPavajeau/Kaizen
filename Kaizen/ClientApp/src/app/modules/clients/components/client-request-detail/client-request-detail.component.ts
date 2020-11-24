import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Client } from '@modules/clients/models/client';
import { ClientState } from '@modules/clients/models/client-state';
import { ClientService } from '@modules/clients/services/client.service';

@Component({
  selector: 'app-client-request-detail',
  templateUrl: './client-request-detail.component.html',
  styleUrls: [ './client-request-detail.component.scss' ]
})
export class ClientRequestDetailComponent implements OnInit {
  clientRequest: Client;
  updatingClientRequest = false;

  constructor(private clientService: ClientService, private activeRoute: ActivatedRoute, private router: Router) {}

  ngOnInit(): void {
    this.loadClientRequest();
  }

  private loadClientRequest(): void {
    const id = this.activeRoute.snapshot.paramMap.get('id');
    this.clientService.getClient(id).subscribe((client) => {
      this.clientRequest = client;
    });
  }

  acceptClient(): void {
    this.processClient(ClientState.Accepted);
  }

  rejectClient(): void {
    this.processClient(ClientState.Rejected);
  }

  processClient(state: ClientState): void {
    this.clientRequest.state = state;
    this.updatingClientRequest = true;
    this.clientService.updateClient(this.clientRequest).subscribe((clientUpdate) => {
      this.router.navigateByUrl('/clients/requests');
    });
  }
}
