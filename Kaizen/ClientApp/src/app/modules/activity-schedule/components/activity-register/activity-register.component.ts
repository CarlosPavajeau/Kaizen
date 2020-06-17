import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Activity } from '@modules/activity-schedule/models/activity';
import { ActivityScheduleService } from '@modules/activity-schedule/services/activity-schedule.service';
import { Component, OnInit } from '@angular/core';
import { Employee } from '@modules/employees/models/employee';
import { EmployeeService } from '@modules/employees/services/employee.service';
import { IForm } from '@core/models/form';
import { MatDialog } from '@angular/material/dialog';
import { NotificationsService } from '@shared/services/notifications.service';
import { RequestState } from '@modules/service-requests/models/request-state';
import { SelectDateModalComponent } from '@shared/components/select-date-modal/select-date-modal.component';
import { ServiceRequest } from '@modules/service-requests/models/service-request';
import { ServiceRequestService } from '@modules/service-requests/services/service-request.service';

@Component({
	selector: 'app-activity-register',
	templateUrl: './activity-register.component.html',
	styleUrls: [ './activity-register.component.css' ]
})
export class ActivityRegisterComponent implements OnInit, IForm {
	serviceRequest: ServiceRequest;
	techniciansAvailable: Employee[] = [];
	activityForm: FormGroup;

	public get controls(): { [key: string]: AbstractControl } {
		return this.activityForm.controls;
	}

	constructor(
		private serviceRequestService: ServiceRequestService,
		private activityScheduleService: ActivityScheduleService,
		private employeeService: EmployeeService,
		private notificationsService: NotificationsService,
		private activateRoute: ActivatedRoute,
		private formBuilder: FormBuilder,
		public dateDialog: MatDialog,
		private router: Router
	) {}

	ngOnInit(): void {
		this.initForm();

		this.activateRoute.queryParamMap.subscribe((queryParams) => {
			const serviceRequestCode = +queryParams.get('serviceRequest');
			this.serviceRequestService.getServiceRequest(serviceRequestCode).subscribe((serviceRequest) => {
				this.serviceRequest = serviceRequest;
				const serviceCodes = serviceRequest.services.map((service) => service.code);
				this.employeeService
					.getTechniciansAvailable(serviceRequest.date, serviceCodes)
					.subscribe((techniciansAvailable) => {
						this.techniciansAvailable = techniciansAvailable;
					});
			});
		});
	}

	initForm(): void {
		this.activityForm = this.formBuilder.group({
			employeeCodes: [ '', [ Validators.required ] ]
		});
	}

	onSubmit(): void {
		if (this.activityForm.valid) {
			const activity = this.mapActivity();
			this.activityScheduleService.saveActivity(activity).subscribe((activitySave) => {
				if (activitySave) {
					this.notificationsService.addMessage(`Actividad N° ${activitySave.code} registrada`, 'Ok');
					this.serviceRequest.state = RequestState.Accepted;
					this.serviceRequestService
						.updateServiceRequest(this.serviceRequest)
						.subscribe((serviceRequestUpdate) => {
							this.router.navigateByUrl('/service_requests');
						});
				}
			});
		}
	}

	private mapActivity(): Activity {
		return {
			date: this.serviceRequest.date,
			state: RequestState.Pending,
			clientId: this.serviceRequest.clientId,
			periodicity: this.serviceRequest.periodicity,
			serviceCodes: this.serviceRequest.services.map((s) => s.code),
			employeeCodes: this.controls['employeeCodes'].value
		};
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
