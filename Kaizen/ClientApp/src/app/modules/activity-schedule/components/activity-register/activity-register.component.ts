import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { buildIsoDate } from '@app/core/utils/date-utils';
import { zeroPad } from '@app/core/utils/number-utils';
import { PERIODICITIES, Periodicity } from '@app/modules/service-requests/models/periodicity-type';
import { ServiceRequestState } from '@app/modules/service-requests/models/service-request-state';
import { IForm } from '@core/models/form';
import { Activity } from '@modules/activity-schedule/models/activity';
import { ActivityScheduleService } from '@modules/activity-schedule/services/activity-schedule.service';
import { Client } from '@modules/clients/models/client';
import { ClientService } from '@modules/clients/services/client.service';
import { Employee } from '@modules/employees/models/employee';
import { EmployeeService } from '@modules/employees/services/employee.service';
import { ServiceRequest } from '@modules/service-requests/models/service-request';
import { ServiceRequestService } from '@modules/service-requests/services/service-request.service';
import { Service } from '@modules/services/models/service';
import { ServiceService } from '@modules/services/services/service.service';
import { SelectDateModalComponent } from '@shared/components/select-date-modal/select-date-modal.component';
import { NotificationsService } from '@shared/services/notifications.service';
import { ActivityState } from '../../models/activity-state';

@Component({
  selector: 'app-activity-register',
  templateUrl: './activity-register.component.html',
  styleUrls: [ './activity-register.component.css' ]
})
export class ActivityRegisterComponent implements OnInit, IForm {
  serviceRequest: ServiceRequest;
  serviceRequestCode: number;
  techniciansAvailable: Employee[] = [];
  activityForm: FormGroup;

  savingData = false;
  loadingtechniciansAvailable = false;
  fromServiceRequest = false;

  services: Service[];
  periodicities: Periodicity[];
  client: Client;

  public get controls(): { [key: string]: AbstractControl } {
    return this.activityForm.controls;
  }

  constructor(
    private serviceRequestService: ServiceRequestService,
    private activityScheduleService: ActivityScheduleService,
    private employeeService: EmployeeService,
    private serviceService: ServiceService,
    private clientService: ClientService,
    private notificationsService: NotificationsService,
    private activateRoute: ActivatedRoute,
    private formBuilder: FormBuilder,
    public dateDialog: MatDialog,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.activateRoute.queryParamMap.subscribe((queryParams) => {
      const serviceRequestCode = +queryParams.get('serviceRequest');
      if (serviceRequestCode) {
        this.fromServiceRequest = true;
        this.serviceRequestCode = serviceRequestCode;
      } else {
        this.periodicities = PERIODICITIES;

        this.serviceService.getServices().subscribe((services) => {
          this.services = services;
        });
      }

      this.initForm();
    });
  }

  initForm(): void {
    if (this.fromServiceRequest) {
      this.activityForm = this.formBuilder.group({
        employeeCodes: [ '', [ Validators.required ] ]
      });
    } else {
      this.activityForm = this.formBuilder.group({
        clientId: [ '', [ Validators.required, Validators.minLength(8), Validators.maxLength(10) ] ],
        serviceCodes: [ '', [ Validators.required ] ],
        periodicity: [ '', [ Validators.required ] ],
        date: [ '', [ Validators.required ] ],
        time: [ '', [ Validators.required ] ],
        employeeCodes: [ '', Validators.required ]
      });
    }
  }

  onLoadedServiceRequest(serviceRequest: ServiceRequest): void {
    this.serviceRequest = serviceRequest;
    const serviceCodes = this.serviceRequest.services.map((s) => s.code);
    this.employeeService
      .getTechniciansAvailable(this.serviceRequest.date, serviceCodes)
      .subscribe((techniciansAvailable) => {
        this.techniciansAvailable = techniciansAvailable;
      });
  }

  loadTechniciansAvailable(): void {
    const validForm = this.controls['serviceCodes'].valid && this.controls['date'].valid && this.controls['time'].valid;
    if (validForm) {
      this.techniciansAvailable = [];
      this.controls['employeeCodes'].reset();
      const serviceCodes = this.controls['serviceCodes'].value;
      const time = this.controls['time'].value;
      const date = this.controls['date'].value as Date;
      const isoDate = buildIsoDate(date, time);

      this.loadingtechniciansAvailable = true;
      this.employeeService.getTechniciansAvailable(isoDate, serviceCodes).subscribe((techniciansAvailable) => {
        this.techniciansAvailable = techniciansAvailable;
        this.loadingtechniciansAvailable = false;
      });
    }
  }

  onSubmit(): void {
    if (this.activityForm.valid) {
      this.savingData = true;
      const activity = this.mapActivity();
      this.activityScheduleService.saveActivity(activity).subscribe((activitySave) => {
        if (activitySave) {
          if (this.fromServiceRequest) {
            this.serviceRequest.state = ServiceRequestState.Accepted;
            this.serviceRequestService.updateServiceRequest(this.serviceRequest).subscribe((serviceRequestUpdate) => {
              this.onSaveActivity(activitySave.code, '/service_requests');
            });
          } else {
            this.onSaveActivity(activitySave.code, '/activity_schedule');
          }
        }
      });
    }
  }

  private onSaveActivity(activityCode: number, url: string): void {
    this.notificationsService.showSuccessMessage(
      `Actividad N° ${activityCode} agendada con éxito. Tambíen se agendaron todas las actividades siguientes dependiendo de la periodicidad de la actividad`,
      () => {
        this.router.navigateByUrl(url);
      }
    );
  }

  private mapActivity(): Activity {
    if (this.fromServiceRequest) {
      const isoDate = buildIsoDate(
        this.serviceRequest.date,
        `${zeroPad(this.serviceRequest.date.getHours(), 2)}:${zeroPad(this.serviceRequest.date.getMinutes(), 2)}`
      );

      return {
        date: isoDate,
        state: ActivityState.Pending,
        clientId: this.serviceRequest.clientId,
        periodicity: this.serviceRequest.periodicity,
        serviceCodes: this.serviceRequest.services.map((s) => s.code),
        employeeCodes: this.controls['employeeCodes'].value
      };
    } else {
      const serviceCodes = this.controls['serviceCodes'].value;
      const time = this.controls['time'].value;
      const date = this.controls['date'].value as Date;
      const isoDate = buildIsoDate(date, time);
      return {
        date: isoDate,
        state: ActivityState.Pending,
        clientId: this.controls['clientId'].value,
        periodicity: +this.controls['periodicity'].value,
        serviceCodes: serviceCodes,
        employeeCodes: this.controls['employeeCodes'].value
      };
    }
  }

  cancelServiceRequest(): void {
    this.serviceRequest.state = ServiceRequestState.Rejected;
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
        this.serviceRequest.state = ServiceRequestState.PendingSuggestedDate;
        this.serviceRequestService.updateServiceRequest(this.serviceRequest).subscribe((serviceRequestUpdate) => {
          if (serviceRequestUpdate) {
            this.router.navigateByUrl('/service_requests');
          }
        });
      }
    });
  }

  searchClient(): void {
    if (this.controls['clientId'].valid) {
      const clientId = this.controls['clientId'].value;
      this.clientService.getClient(clientId).subscribe((client) => {
        this.client = client;
      });
    }
  }
}
