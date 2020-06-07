import { Component, OnInit } from '@angular/core';
import { RequestState } from '@modules/service-requests/models/request-state';
import { ServiceRequest } from '@modules/service-requests/models/service-request';
import { ServiceRequestService } from '@modules/service-requests/services/service-request.service';

@Component({
	selector: 'app-service-requests',
	templateUrl: './service-requests.component.html',
	styleUrls: [ './service-requests.component.css' ]
})
export class ServiceRequestsComponent implements OnInit {
	serviceRequests: ServiceRequest[];

	constructor(private serviceRequestService: ServiceRequestService) {}

	ngOnInit(): void {
		this.loadServiceRequests();
	}

	private loadServiceRequests(): void {
		this.serviceRequestService.getServiceRequests().subscribe((serviceRequests) => {
			this.serviceRequests = serviceRequests;
		});
	}

	rejectServiceRequest(serviceRequest: ServiceRequest): void {
		serviceRequest.state = RequestState.Rejected;
		this.serviceRequestService.updateServiceRequest(serviceRequest).subscribe((serviceRequestUpdate) => {
			if (serviceRequestUpdate) {
				this.serviceRequests = this.serviceRequests.filter((s) => s.code != serviceRequestUpdate.code);
			}
		});
	}
}
