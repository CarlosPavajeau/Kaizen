import { AuthenticationService } from '@core/authentication/authentication.service';
import { CLIENT_ROLE } from '@app/global/roles';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Person } from '@shared/models/person';
import { ServiceInvoice } from '../../models/service-invoice';
import { ServiceInvoiceService } from '../../services/service-invoice.service';

@Component({
	selector: 'app-service-invoices',
	templateUrl: './service-invoices.component.html',
	styleUrls: [ './service-invoices.component.css' ]
})
export class ServiceInvoicesComponent implements OnInit {
	serviceInvoices: ServiceInvoice[];
	dataSource: MatTableDataSource<ServiceInvoice> = new MatTableDataSource<ServiceInvoice>(this.serviceInvoices);
	displayedColumns: string[] = [ 'id', 'state', 'iva', 'subtotal', 'total', 'actions' ];
	@ViewChild(MatPaginator, { static: true })
	paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	isClient = false;

	constructor(private authService: AuthenticationService, private serviceInvoiceService: ServiceInvoiceService) {}

	ngOnInit(): void {
		this.loadData();
	}

	private loadData(): void {
		const role = this.authService.getUserRole();
		if (role === CLIENT_ROLE) {
			const currentPerson: Person = JSON.parse(localStorage.getItem('current_person'));
			this.serviceInvoiceService.getClientServiceInvoices(currentPerson.id).subscribe((servicesInvoices) => {
				this.serviceInvoices = servicesInvoices;
				this.dataSource.data = servicesInvoices;
				this.isClient = true;
			});
		} else {
			this.serviceInvoiceService.getServiceInvoices().subscribe((serviceInvoices) => {
				this.serviceInvoices = serviceInvoices;
				this.dataSource.data = serviceInvoices;
			});
		}
	}
}
