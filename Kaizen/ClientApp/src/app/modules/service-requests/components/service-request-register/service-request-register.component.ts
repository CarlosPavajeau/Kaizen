import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthenticationService } from '@core/authentication/authentication.service';
import { ClientService } from '@modules/clients/services/client.service';
import { Component, OnInit } from '@angular/core';
import { IForm } from '@core/models/form';
import { NotificationsService } from '@shared/services/notifications.service';
import { PERIODICITIES, Periodicity } from '@modules/service-requests/models/periodicity-type';
import { RequestState } from '@modules/service-requests/models/request-state';
import { Router } from '@angular/router';
import { Service } from '@app/modules/services/models/service';
import { ServiceRequest } from '@modules/service-requests/models/service-request';
import { ServiceRequestService } from '@modules/service-requests/services/service-request.service';
import { ServiceService } from '@modules/services/services/service.service';
import { zeroPad } from '@core/utils/number-utils';

@Component({
	selector: 'app-service-request-register',
	templateUrl: './service-request-register.component.html',
	styleUrls: [ './service-request-register.component.css' ]
})
export class ServiceRequestRegisterComponent implements OnInit, IForm {
	serviceRequestForm: FormGroup;
	services: Service[];
	periodicities: Periodicity[];
	private clientId: string;

	get controls(): { [key: string]: AbstractControl } {
		return this.serviceRequestForm.controls;
	}

	constructor(
		private serviceRequestService: ServiceRequestService,
		private serviceService: ServiceService,
		private clientService: ClientService,
		private authService: AuthenticationService,
		private notificationService: NotificationsService,
		private formBuilder: FormBuilder,
		private router: Router
	) {}

	ngOnInit(): void {
		this.loadData();
		this.initForm();
	}

	private loadData(): void {
		this.periodicities = PERIODICITIES;

		const user_id = this.authService.getCurrentUser().id;
		this.clientService.getClientId(user_id).subscribe((clientId) => {
			this.clientId = clientId + '';
		});

		this.serviceService.getServices().subscribe((services) => {
			this.services = services;
		});
	}

	initForm(): void {
		this.serviceRequestForm = this.formBuilder.group({
			date: [ '', [ Validators.required ] ],
			time: [ '', [ Validators.required ] ],
			serviceCodes: [ '', [ Validators.required ] ],
			periodicity: [ '', Validators.required ]
		});
	}

	onSubmit(): void {
		if (this.serviceRequestForm.valid) {
			const serviceRequest = this.mapServiceRequest();
			this.serviceRequestService.saveServiceRequest(serviceRequest).subscribe((serviceRequestSave) => {
				if (serviceRequestSave) {
					this.notificationService.addMessage(
						`Solicitud de servicio N° ${serviceRequestSave.code} registrada`,
						'Ok'
					);
					setTimeout(() => {
						this.router.navigateByUrl('/user/profile');
					}, 2000);
				}
			});
		}
	}

	private mapServiceRequest(): ServiceRequest {
		const time = this.controls['time'].value;
		const date = this.controls['date'].value as Date;
		const isoDate = new Date(
			`${date.getFullYear()}-${zeroPad(date.getMonth() + 1, 2)}-${zeroPad(date.getDate(), 2)}T${time}:00Z`
		);
		return {
			clientId: this.clientId,
			serviceCodes: this.controls['serviceCodes'].value,
			date: isoDate,
			state: RequestState.Pending,
			periodicity: this.controls['periodicity'].value
		};
	}
}
