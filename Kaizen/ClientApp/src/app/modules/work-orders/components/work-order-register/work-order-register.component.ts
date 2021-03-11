import { Component, OnInit, ViewChild } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { IForm } from '@core/models/form';
import { zeroPad } from '@core/utils/number-utils';
import { Activity } from '@modules/activity-schedule/models/activity';
import { ActivityScheduleService } from '@modules/activity-schedule/services/activity-schedule.service';
import { Employee } from '@modules/employees/models/employee';
import { Sector } from '@modules/work-orders/models/sector';
import { WorkOrder } from '@modules/work-orders/models/work-order';
import { WorkOrderState } from '@modules/work-orders/models/work-order-state';
import { WorkOrderService } from '@modules/work-orders/service/work-order.service';
import { DigitalSignatureComponent } from '@shared/components/digital-signature/digital-signature.component';
import { ObservableStatus } from '@shared/models/observable-with-status';
import { DialogsService } from '@shared/services/dialogs.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Component({
  selector: 'app-work-order-register',
  templateUrl: './work-order-register.component.html',
  styleUrls: [ './work-order-register.component.scss' ]
})
export class WorkOrderRegisterComponent implements OnInit, IForm {
  public ObsStatus: typeof ObservableStatus = ObservableStatus;
  public WorkOrderState: typeof WorkOrderState = WorkOrderState;

  sectors$: Observable<Sector[]>;
  activity$: Observable<Activity>;
  workOrder$: Observable<WorkOrder>;
  workOrder: WorkOrder;

  workOrderForm: FormGroup;

  @ViewChild('digitalSignature') digitalSignature: DigitalSignatureComponent;

  savingOrUpdatingData = false;

  get controls(): { [key: string]: AbstractControl } {
    return this.workOrderForm.controls;
  }

  constructor(
    private workOrderService: WorkOrderService,
    private activityService: ActivityScheduleService,
    private formBuilder: FormBuilder,
    private activateRoute: ActivatedRoute,
    private router: Router,
    private dialogsService: DialogsService
  ) {
  }

  ngOnInit(): void {
    this.initForm();
    this.loadData();
  }

  initForm(): void {
    this.workOrderForm = this.formBuilder.group({
      arrivalTime: [ '', [ Validators.required ] ],
      departureTime: [ '', [ Validators.required ] ],
      sector: [ '', [ Validators.required ] ],
      observations: [ '', [ Validators.required, Validators.maxLength(500), Validators.minLength(30) ] ]
    });
  }

  private loadData(): void {
    this.sectors$ = this.workOrderService.getSectors();

    this.activateRoute.queryParamMap.subscribe((queryParams) => {
      const activityCode = +queryParams.get('activity');
      this.activity$ = this.activityService.getActivity(activityCode);

      this.workOrder$ = this.workOrderService.getWorkOrderOfActivity(activityCode).pipe(catchError(() => of(null)));
      this.workOrder$.subscribe((workOrder) => {
        this.workOrder = workOrder;
      });
    });
  }

  generateWorkOrder(activity: Activity): void {
    const validForm = this.controls['arrivalTime'].valid && this.controls['sector'].valid;
    if (validForm) {
      const arrivalTime = this.controls['arrivalTime'].value;
      const date = new Date();
      const arrivalTimeISO = new Date(
        `${ date.getFullYear() }-${ zeroPad(date.getMonth() + 1, 2) }-${ zeroPad(date.getDate(), 2) }T${ arrivalTime }:00Z`
      );

      const executionTime = `${ zeroPad(date.getHours(), 2) }:${ zeroPad(date.getMinutes(), 2) }`;
      const executionDate = new Date(
        `${ date.getFullYear() }-${ zeroPad(date.getMonth() + 1, 2) }-${ zeroPad(date.getDate(), 2) }T${ executionTime }:00Z`
      );

      const employee: Employee = JSON.parse(localStorage.getItem('current_person'));

      const workOrder: WorkOrder = {
        workOrderState: WorkOrderState.Generated,
        arrivalTime: arrivalTimeISO,
        activityCode: activity.code,
        employeeId: employee.id,
        sectorId: +this.controls['sector'].value,
        executionDate: executionDate,
        departureTime: date,
        validity: date
      };

      this.savingOrUpdatingData = true;
      this.workOrderService.saveWorkOrder(workOrder).subscribe((workOrderSave) => {
        if (workOrderSave) {
          this.dialogsService.showSuccessDialog(
            `Orden de trabajo N°${ workOrderSave.code } generada con éxito.`,
            () => {
              this.workOrder = workOrderSave;
              this.savingOrUpdatingData = false;
            }
          );
        }
      });
    }
  }

  confirmWorkOrder(): void {
    if (!this.digitalSignature.isEmpty) {
      if (this.workOrder) {
        const workOrder: WorkOrder = {
          ...this.workOrder,
          clientSignature: this.digitalSignature.getImageData(),
          workOrderState: WorkOrderState.Confirmed
        };

        this.savingOrUpdatingData = true;
        this.workOrderService.updateWorkOrder(workOrder).subscribe((workOrderUpdate) => {
          if (workOrderUpdate) {
            this.dialogsService.showSuccessDialog(
              `Orden de trabajo N°${ workOrderUpdate.code } confirmada correctamente.`,
              () => {
                this.workOrder = workOrderUpdate;
                this.savingOrUpdatingData = false;
              }
            );
          }
        });
      }
    }
  }

  cancelWorkOrder(): void {
    this.workOrder.workOrderState = WorkOrderState.Canceled;

    this.savingOrUpdatingData = true;
    this.workOrderService.updateWorkOrder(this.workOrder).subscribe((workOrderUpdate) => {
      if (workOrderUpdate) {
        this.dialogsService.showSuccessDialog(
          `Orden de trabajo N°${ workOrderUpdate.code } cancelada correctamente.`,
          () => {
            this.router.navigateByUrl('/user/profile');
          }
        );
      }
    });
  }

  saveWorkOrder(): void {
    const validForm = this.controls['observations'].valid && this.controls['departureTime'].valid;
    if (validForm) {
      this.workOrder.observations = this.controls['observations'].value;

      const departureTime = this.controls['departureTime'].value;
      const date = new Date();
      this.workOrder.departureTime = new Date(
        `${ date.getFullYear() }-${ zeroPad(date.getMonth() + 1, 2) }-${ zeroPad(date.getDate(), 2) }T${ departureTime }:00Z`
      );

      this.workOrder.workOrderState = WorkOrderState.Valid;

      this.savingOrUpdatingData = true;
      this.workOrderService.updateWorkOrder(this.workOrder).subscribe((workOrderUpdate) => {
        if (workOrderUpdate) {
          this.dialogsService.showSuccessDialog(
            `Orden de trabajo N°${ workOrderUpdate.code } guardada correctamente.`,
            () => {
              this.router.navigateByUrl('/user/profile');
            }
          );
        }
      });
    }
  }

  clearSignaturePad(): void {
    this.digitalSignature.clear();
  }
}
