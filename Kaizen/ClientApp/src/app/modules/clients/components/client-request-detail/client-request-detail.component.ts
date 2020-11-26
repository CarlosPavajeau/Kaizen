import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Client } from '@modules/clients/models/client';
import { ClientState } from '@modules/clients/models/client-state';
import { ClientService } from '@modules/clients/services/client.service';
import { ObservableStatus } from '@shared/models/observable-with-status';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-client-request-detail',
  templateUrl: './client-request-detail.component.html',
  styleUrls: [ './client-request-detail.component.scss' ]
})
export class ClientRequestDetailComponent implements OnInit {
  public ObsStatus: typeof ObservableStatus = ObservableStatus;

  clientRequest$: Observable<Client>;
  updatingClientRequest = false;

  constructor(private clientService: ClientService, private activeRoute: ActivatedRoute, private router: Router) {}

  ngOnInit(): void {
    this.loadClientRequest();
  }

  private loadClientRequest(): void {
    const id = this.activeRoute.snapshot.paramMap.get('id');
    this.clientRequest$ = this.clientService.getClient(id);
  }

  acceptClient(client: Client): void {
    this.processClient(client, ClientState.Accepted);
  }

  rejectClient(client: Client): void {
    this.processClient(client, ClientState.Rejected);
  }

  processClient(client: Client, state: ClientState): void {
    client.state = state;
    this.updatingClientRequest = true;
    this.clientService.updateClient(client).subscribe((clientUpdate) => {
      this.router.navigateByUrl('/clients/requests');
    });
  }
}
