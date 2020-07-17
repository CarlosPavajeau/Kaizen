import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';

import { Client } from '@modules/clients/models/client';
import { ClientService } from '@modules/clients/services/client.service';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';

@Component({
	selector: 'app-clients',
	templateUrl: './clients.component.html',
	styleUrls: [ './clients.component.css' ]
})
export class ClientsComponent implements OnInit, AfterViewInit {
	clients: Client[];
	dataSource: MatTableDataSource<Client> = new MatTableDataSource<Client>(this.clients);
	displayedColumns: string[] = [ 'id', 'name', 'clientType', 'phonenumber', 'options' ];
	@ViewChild(MatPaginator, { static: true })
	paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;

	constructor(private clientService: ClientService) {}

	ngOnInit(): void {
		this.loadClients();
	}

	loadClients(): void {
		this.clientService.getClients().subscribe((clients) => {
			this.clients = clients;
			this.dataSource.data = this.clients;
		});
	}

	ngAfterViewInit(): void {
		this.dataSource.paginator = this.paginator;
		this.dataSource.sort = this.sort;
	}

	applyFilter(filterValue: string) {
		filterValue = filterValue.trim().toLowerCase();
		this.dataSource.filter = filterValue;
	}
}
