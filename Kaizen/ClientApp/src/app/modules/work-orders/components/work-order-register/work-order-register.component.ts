import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Component, OnInit, ViewChild } from '@angular/core';
import { IForm } from '@core/models/form';
import { Sector } from '@modules/work-orders/models/sector';
import { WorkOrder } from '@modules/work-orders/models/work-order';
import { WorkOrderService } from '@modules/work-orders/service/work-order.service';
import { ActivatedRoute } from '@angular/router';
import { ActivityScheduleService } from '@app/modules/activity-schedule/services/activity-schedule.service';
import { Activity } from '@app/modules/activity-schedule/models/activity';
import * as moment from 'moment';
import { DigitalSignatureComponent } from '@shared/components/digital-signature/digital-signature.component';
@Component({
	selector: 'app-work-order-register',
	templateUrl: './work-order-register.component.html',
	styleUrls: [ './work-order-register.component.css' ]
})
export class WorkOrderRegisterComponent implements OnInit, IForm {
	workOrderForm: FormGroup;
	sectors: Sector[] = [];
	activity: Activity;

	@ViewChild('digitalSignature') digitalSignature: DigitalSignatureComponent;

	get controls(): { [key: string]: AbstractControl } {
		return this.workOrderForm.controls;
	}

	constructor(
		private workOrderService: WorkOrderService,
		private activityService: ActivityScheduleService,
		private formBuilder: FormBuilder,
		private activateRoute: ActivatedRoute
	) {}

	ngOnInit(): void {
		this.initForm();
		this.loadData();
	}

	initForm(): void {
		this.workOrderForm = this.formBuilder.group({
			arrivalTime: [ '', [ Validators.required ] ],
			depatureTime: [ '', [ Validators.required ] ],
			sector: [ '', [ Validators.required ] ],
			observations: [ '', [ Validators.required ] ]
		});
	}

	private loadData(): void {
		this.workOrderService.getSectors().subscribe((sectors) => {
			this.sectors = sectors;
		});

		this.activateRoute.queryParamMap.subscribe((queryParams) => {
			const activityCode = +queryParams.get('activity');
			this.activityService.getActivity(activityCode).subscribe((activity) => {
				this.activity = activity;
				console.log(activity);
			});
		});
	}

	onSubmit(): void {
		console.log(this.digitalSignature.getImageData());
	}

	private mapWorkOrder(): WorkOrder {
		return null;
	}
}
