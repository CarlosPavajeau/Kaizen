import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { Employee } from '@modules/employees/models/employee';
import { EmployeeService } from '@modules/employees/services/employee.service';
import { RequestState } from '@modules/service-requests/models/request-state';
import { ServiceRequest } from '@modules/service-requests/models/service-request';
import { ServiceRequestService } from '@modules/service-requests/services/service-request.service';
import { MatDialog } from '@angular/material/dialog';
import { SelectDateModalComponent } from '@app/shared/components/select-date-modal/select-date-modal.component';

@Component({
	selector: 'app-service-request-detail',
	templateUrl: './service-request-detail.component.html',
	styleUrls: [ './service-request-detail.component.css' ]
})
export class ServiceRequestDetailComponent implements OnInit {
	serviceRequest: ServiceRequest;
	techniciansAvailable: Employee[] = [];

	constructor(
		private serviceRequestService: ServiceRequestService,
		private employeeService: EmployeeService,
		private activateRoute: ActivatedRoute,
		private router: Router,
		public dateDialog: MatDialog
	) {}

	ngOnInit(): void {
		const code = +this.activateRoute.snapshot.paramMap.get('code');
		this.serviceRequestService.getServiceRequest(code).subscribe((serviceRequest) => {
			this.serviceRequest = serviceRequest;
			const serviceCodes = serviceRequest.services.map((service) => service.code);
			this.employeeService
				.getTechniciansAvailable(serviceRequest.date, serviceCodes)
				.subscribe((techniciansAvailable) => {
					this.techniciansAvailable = techniciansAvailable;
				});
		});
	}

	cancelServiceRequest(): void {
		this.serviceRequest.state = RequestState.Rejected;
		this.serviceRequestService.updateServiceRequest(this.serviceRequest).subscribe((serviceRequestUpdate) => {
			if (serviceRequestUpdate) {
				this.router.navigateByUrl('/service_requests');
			}
		});
	}

	suggestAnotherDate(): void {
		const dateRef = this.dateDialog.open(SelectDateModalComponent, {
			width: '700px'
		});

		dateRef.afterClosed().subscribe((date) => {
			if (date) {
				this.serviceRequest.date = date;
				this.serviceRequest.state = RequestState.PendingSuggestedDate;
				this.serviceRequestService
					.updateServiceRequest(this.serviceRequest)
					.subscribe((serviceRequestUpdate) => {
						if (serviceRequestUpdate) {
							this.router.navigateByUrl('/service_requests');
						}
					});
			}
		});
	}
}
