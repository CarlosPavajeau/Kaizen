import { Component, OnInit } from '@angular/core';
import { ServiceRequestService } from '../../services/service-request.service';
import { ServiceRequest } from '../../models/service-request';
import { EmployeeService } from '@app/modules/employees/services/employee.service';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { Employee } from '@app/modules/employees/models/employee';
import { RequestState } from '../../models/request-state';
import { SelectDateModalComponent } from '@app/shared/components/select-date-modal/select-date-modal.component';

@Component({
	selector: 'app-service-request-process',
	templateUrl: './service-request-process.component.html',
	styleUrls: [ './service-request-process.component.css' ]
})
export class ServiceRequestProcessComponent implements OnInit {
	serviceRequest: ServiceRequest;
	techniciansAvailable: Employee[] = [];
	serviceRequestCode: number;

	constructor(
		private serviceRequestService: ServiceRequestService,
		private employeeService: EmployeeService,
		private activateRoute: ActivatedRoute,
		private router: Router,
		public dateDialog: MatDialog
	) {}

	ngOnInit(): void {
		this.loadData();
	}

	private loadData(): void {
		const code = +this.activateRoute.snapshot.paramMap.get('code');
		this.serviceRequestCode = code;
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

	onLoadedServiceRequest(serviceRequest: ServiceRequest): void {
		this.serviceRequest = serviceRequest;
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
