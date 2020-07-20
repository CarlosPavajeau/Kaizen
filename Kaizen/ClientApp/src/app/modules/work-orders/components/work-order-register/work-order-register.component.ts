import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Activity } from '@modules/activity-schedule/models/activity';
import { ActivityScheduleService } from '@modules/activity-schedule/services/activity-schedule.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { DigitalSignatureComponent } from '@shared/components/digital-signature/digital-signature.component';
import { Employee } from '@modules/employees/models/employee';
import { IForm } from '@core/models/form';
import { Sector } from '@modules/work-orders/models/sector';
import { WorkOrder } from '@modules/work-orders/models/work-order';
import { WorkOrderService } from '@modules/work-orders/service/work-order.service';
import { WorkOrderState } from '@modules/work-orders/models/work-order-state';
import { zeroPad } from '@core/utils/number-utils';

@Component({
	selector: 'app-work-order-register',
	templateUrl: './work-order-register.component.html',
	styleUrls: [ './work-order-register.component.css' ]
})
export class WorkOrderRegisterComponent implements OnInit, IForm {
	workOrderForm: FormGroup;
	sectors: Sector[] = [];
	activity: Activity;
	workOrder: WorkOrder;

	@ViewChild('digitalSignature') digitalSignature: DigitalSignatureComponent;

	get controls(): { [key: string]: AbstractControl } {
		return this.workOrderForm.controls;
	}

	constructor(
		private workOrderService: WorkOrderService,
		private activityService: ActivityScheduleService,
		private formBuilder: FormBuilder,
		private activateRoute: ActivatedRoute,
		private router: Router
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
			observations: [ '', [ Validators.required, Validators.maxLength(500), Validators.minLength(30) ] ]
		});
	}

	private loadData(): void {
		this.workOrderService.getSectors().subscribe((sectors) => {
			this.sectors = sectors;
		});

		this.activateRoute.queryParamMap.subscribe((queryParams) => {
			const activityCode = +queryParams.get('activity');

			this.workOrderService.getWorkOrderOfActivity(activityCode).subscribe((workOrder) => {
				this.workOrder = workOrder;
			});

			this.activityService.getActivity(activityCode).subscribe((activity) => {
				this.activity = activity;
			});
		});
	}

	generateWorkOrder(): void {
		const validForm = this.controls['arrivalTime'].valid && this.controls['sector'].valid;
		if (validForm) {
			const arrivalTime = this.controls['arrivalTime'].value;
			const date = new Date();
			const arrivalTimeISO = new Date(
				`${date.getFullYear()}-${zeroPad(date.getMonth() + 1, 2)}-${zeroPad(
					date.getDate(),
					2
				)}T${arrivalTime}:00Z`
			);

			const executionTime = `${zeroPad(date.getHours(), 2)}:${zeroPad(date.getMinutes(), 2)}`;
			const executionDate = new Date(
				`${date.getFullYear()}-${zeroPad(date.getMonth() + 1, 2)}-${zeroPad(
					date.getDate(),
					2
				)}T${executionTime}:00Z`
			);

			const employee: Employee = JSON.parse(localStorage.getItem('current_person'));

			const workOrder: WorkOrder = {
				workOrderState: WorkOrderState.Generated,
				arrivalTime: arrivalTimeISO,
				activityCode: this.activity.code,
				employeeId: employee.id,
				sectorId: +this.controls['sector'].value,
				executionDate: executionDate,
				depatureTime: date,
				validity: date
			};

			this.workOrderService.saveWorkOrder(workOrder).subscribe((workOrderSave) => {
				if (workOrderSave) {
					this.workOrder = workOrderSave;
				}
			});
		}
	}

	confirmWorkOrder(): void {
		if (!this.digitalSignature.isEmpty) {
			if (this.workOrder) {
				const workOrder = Object.assign({}, this.workOrder);
				workOrder.clientSignature = this.digitalSignature.getImageData();
				workOrder.workOrderState = WorkOrderState.Confirmed;

				this.workOrderService.updateWorkOrder(workOrder).subscribe((workOrderUpdate) => {
					if (workOrderUpdate) {
						this.workOrder = workOrderUpdate;
					}
				});
			}
		}
	}

	cancelWorkOrder(): void {
		this.workOrder.workOrderState = WorkOrderState.Canceled;
		this.workOrderService.updateWorkOrder(this.workOrder).subscribe((workOrderUpdate) => {
			if (workOrderUpdate) {
				this.router.navigateByUrl('/user/profile');
			}
		});
	}

	saveWorkOrder(): void {
		const validForm = this.controls['observations'].valid && this.controls['depatureTime'].valid;
		if (validForm) {
			this.workOrder.observations = this.controls['observations'].value;

			const depatureTime = this.controls['depatureTime'].value;
			const date = new Date();
			const depatureTimeISO = new Date(
				`${date.getFullYear()}-${zeroPad(date.getMonth() + 1, 2)}-${zeroPad(
					date.getDate(),
					2
				)}T${depatureTime}:00Z`
			);
			this.workOrder.depatureTime = depatureTimeISO;

			this.workOrder.workOrderState = WorkOrderState.Valid;

			this.workOrderService.updateWorkOrder(this.workOrder).subscribe((workOrderUpdate) => {
				if (workOrderUpdate) {
					this.router.navigateByUrl('/user/profile');
				}
			});
		}
	}

	clearSignaturePad(): void {
		this.digitalSignature.clear();
	}
}
