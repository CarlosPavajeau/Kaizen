import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { IForm } from '@core/models/form';
import { buildIsoDate } from '@core/utils/date-utils';
import { Client } from '@modules/clients/models/client';
import { ClientState } from '@modules/clients/models/client-state';
import { ClientService } from '@modules/clients/services/client.service';
import { PERIODICITIES, Periodicity } from '@modules/service-requests/models/periodicity-type';
import { ServiceRequest } from '@modules/service-requests/models/service-request';
import { ServiceRequestState } from '@modules/service-requests/models/service-request-state';
import { ServiceRequestService } from '@modules/service-requests/services/service-request.service';
import { Service } from '@modules/services/models/service';
import { ServiceService } from '@modules/services/services/service.service';
import { ObservableStatus } from '@shared/models/observable-with-status';
import { NotificationsService } from '@shared/services/notifications.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Component({
  selector: 'app-service-request-register',
  templateUrl: './service-request-register.component.html',
  styleUrls: [ './service-request-register.component.scss' ]
})
export class ServiceRequestRegisterComponent implements OnInit, IForm {
  public ObsStatus: typeof ObservableStatus = ObservableStatus;
  public ClientState: typeof ClientState = ClientState;

  minDate: Date;
  maxDate: Date;

  services$: Observable<Service[]>;
  periodicities: Periodicity[];

  serviceRequest$: Observable<ServiceRequest>;

  private clientId: string;
  client: Client;
  client$: Observable<Client>;

  savingData = false;
  canBeRegisterServiceRequest = false;

  serviceRequestForm: FormGroup;

  get controls(): { [key: string]: AbstractControl } {
    return this.serviceRequestForm.controls;
  }

  constructor(
    private serviceRequestService: ServiceRequestService,
    private clientService: ClientService,
    private serviceService: ServiceService,
    private notificationService: NotificationsService,
    private formBuilder: FormBuilder,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadData();
    this.initForm();

    const currentDate = new Date();
    this.minDate = new Date(currentDate.getFullYear(), currentDate.getMonth(), currentDate.getDate() + 1);
    this.maxDate = new Date(currentDate.getFullYear(), 11, 31);
  }

  private loadData(): void {
    this.client = JSON.parse(localStorage.getItem('current_person'));
    this.client$ = this.clientService.getClient(this.client.id);

    this.client$.subscribe((client) => {
      this.client = client;
      localStorage.setItem('current_person', JSON.stringify(this.client));

      if (this.client.state === ClientState.Accepted || this.client.state === ClientState.Inactive) {
        this.canBeRegisterServiceRequest = true;
        this.serviceRequest$ = this.serviceRequestService.getPendingServiceRequest(this.client.id).pipe(
          catchError(() => {
            return of(null);
          })
        );

        this.serviceRequest$.subscribe((pendingRequest) => {
          if (pendingRequest && pendingRequest.state === ServiceRequestState.PendingSuggestedDate) {
            this.router.navigateByUrl('/service_requests/new_date');
          } else {
            this.periodicities = PERIODICITIES;
            this.clientId = this.client.id;

            this.services$ = this.serviceService.getServices();
          }
        });
      }
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
      this.savingData = true;
      this.serviceRequestService.saveServiceRequest(serviceRequest).subscribe((serviceRequestSave) => {
        if (serviceRequestSave) {
          this.notificationService.showSuccessMessage(
            `Solicitud de servicio N° ${serviceRequestSave.code} registrada. Espere nuestra pronta respuesta.`,
            () => {
              this.router.navigateByUrl('/user/profile');
            }
          );
        } else {
          this.notificationService.showErrorMessage(
            'La solicitud de servicio no pudo ser registrada. Por favor verifique que los campos enviados son correctos.'
          );
        }
      });
    }
  }

  private mapServiceRequest(): ServiceRequest {
    const time = this.controls['time'].value;
    const date = this.controls['date'].value as Date;
    const isoDate = buildIsoDate(date, time);
    return {
      ...this.serviceRequestForm.value,
      clientId: this.clientId,
      date: isoDate,
      state: ServiceRequestState.Pending
    };
  }
}
