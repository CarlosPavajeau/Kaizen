import { Component, OnInit } from '@angular/core';
import { Client } from '@modules/clients/models/client';
import { ClientService } from '../../services/client.service';
import { ActivatedRoute } from '@angular/router';

@Component({
	selector: 'app-client-detail',
	templateUrl: './client-detail.component.html',
	styleUrls: [ './client-detail.component.css' ]
})
export class ClientDetailComponent implements OnInit {
	client: Client;

	constructor(private clientService: ClientService, private activatedRoute: ActivatedRoute) {}

	ngOnInit(): void {
		this.loadData();
	}

	private loadData(): void {
		const id = this.activatedRoute.snapshot.paramMap.get('id');

		this.clientService.getClient(id).subscribe((client) => {
			this.client = client;
		});
	}
}
