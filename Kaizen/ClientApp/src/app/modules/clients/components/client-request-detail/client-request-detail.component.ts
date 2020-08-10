import { Component, OnInit } from '@angular/core';
import { ClientService } from '../../services/client.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Client } from '../../models/client';
import { ClientState } from '../../models/client-state';

@Component({
  selector: 'app-client-request-detail',
  templateUrl: './client-request-detail.component.html',
  styleUrls: [ './client-request-detail.component.css' ]
})
export class ClientRequestDetailComponent implements OnInit {
  clientRequest: Client;

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
    this.proccessClient(ClientState.Acceptep);
  }

  rejectClient(): void {
    this.proccessClient(ClientState.Rejected);
  }

  proccessClient(state: ClientState): void {
    this.clientRequest.state = state;
    this.clientService.updateClient(this.clientRequest).subscribe((clientUpdate) => {
      this.router.navigateByUrl('/clients/requests');
    });
  }
}
