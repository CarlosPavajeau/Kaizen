import { Component, OnInit } from '@angular/core';
import { ClientService } from '../../services/client.service';
import { Client } from '../../models/client';
import { ClientState } from '../../models/client-state';

@Component({
	selector: 'app-client-requests',
	templateUrl: './client-requests.component.html',
	styleUrls: [ './client-requests.component.css' ]
})
export class ClientRequestsComponent implements OnInit {
	clientRequests: Client[];
	constructor(private clientService: ClientService) {}

	ngOnInit(): void {
		this.loadClientRequests();
	}

	private loadClientRequests(): void {
		this.clientService.getClientRequests().subscribe((clientRequests) => {
			this.clientRequests = clientRequests;
		});
	}

	acceptClient(client: Client): void {
		client.state = ClientState.Acceptep;
		this.proccessClient(client);
	}

	rejectClient(client: Client): void {
		client.state = ClientState.Rejected;
		this.proccessClient(client);
	}

	private proccessClient(client: Client): void {
		this.clientService.updateClient(client).subscribe((clientUpdate) => {
			this.clientRequests = this.clientRequests.filter((c) => c.id != clientUpdate.id);
		});
	}
}
