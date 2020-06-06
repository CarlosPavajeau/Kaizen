import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, AbstractControl, Validators } from '@angular/forms';
import { ServiceRequestService } from '@modules/service-requests/services/service-request.service';
import { ServiceService } from '@modules/services/services/service.service';
import { IForm } from '@app/core/models/form';
import { Service } from '@app/modules/services/models/service';
import { Periodicity, PERIODICITIES } from '../../models/periodicity-type';

@Component({
	selector: 'app-service-request-register',
	templateUrl: './service-request-register.component.html',
	styleUrls: [ './service-request-register.component.css' ]
})
export class ServiceRequestRegisterComponent implements OnInit, IForm {
	serviceRequestForm: FormGroup;
	services: Service[];
	periodicities: Periodicity[];

	get controls(): { [key: string]: AbstractControl } {
		return this.serviceRequestForm.controls;
	}

	constructor(
		private serviceRequestService: ServiceRequestService,
		private serviceService: ServiceService,
		private formBuilder: FormBuilder
	) {}

	ngOnInit(): void {
		this.loadData();
		this.initForm();
	}

	private loadData(): void {
		this.periodicities = PERIODICITIES;
		this.serviceService.getServices().subscribe((services) => {
			this.services = services;
		});
	}

	initForm(): void {
		this.serviceRequestForm = this.formBuilder.group({
			date: [ '', [ Validators.required ] ],
			serviceCodes: [ '', [ Validators.required ] ],
			periodicity: [ '', Validators.required ]
		});
	}
}
