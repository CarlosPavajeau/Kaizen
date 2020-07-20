import { Client } from '@modules/clients/models/client';
import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ServiceRequestState } from '../../models/service-request-state';
import { Router } from '@angular/router';
import { SelectDateModalComponent } from '@shared/components/select-date-modal/select-date-modal.component';
import { ServiceRequest } from '../../models/service-request';
import { ServiceRequestService } from '../../services/service-request.service';

@Component({
	selector: 'app-service-request-new-date',
	templateUrl: './service-request-new-date.component.html',
	styleUrls: [ './service-request-new-date.component.css' ]
})
export class ServiceRequestNewDateComponent implements OnInit {
	serviceRequest: ServiceRequest;

	constructor(
		private serviceRequestService: ServiceRequestService,
		private router: Router,
		private dateDialog: MatDialog
	) {}

	ngOnInit(): void {
		this.loadData();
	}

	private loadData(): void {
		const client: Client = JSON.parse(localStorage.getItem('current_person'));

		this.serviceRequestService.getPendingServiceRequest(client.id).subscribe((pendingRequest) => {
			this.serviceRequest = pendingRequest;
		});
	}

	suggestAnotherDate(): void {
		const dateRef = this.dateDialog.open(SelectDateModalComponent, {
			width: '700px'
		});

		dateRef.afterClosed().subscribe((date) => {
			if (date) {
				this.serviceRequest.date = date;
				this.acceptSuggestedDate();
			}
		});
	}

	acceptSuggestedDate(): void {
		this.serviceRequest.state = ServiceRequestState.Pending;
		this.updateServiceRequest();
	}

	private updateServiceRequest(): void {
		this.serviceRequestService.updateServiceRequest(this.serviceRequest).subscribe((serviceRequestUpdate) => {
			this.router.navigateByUrl('/user/profile');
		});
	}
}
