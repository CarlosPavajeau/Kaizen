import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { Client } from '@modules/clients/models/client';
import { ServiceRequest } from '@modules/service-requests/models/service-request';
import { ServiceRequestState } from '@modules/service-requests/models/service-request-state';
import { ServiceRequestService } from '@modules/service-requests/services/service-request.service';
import { SelectDateModalComponent } from '@shared/components/select-date-modal/select-date-modal.component';
import { ObservableStatus } from '@shared/models/observable-with-status';
import { DialogsService } from '@shared/services/dialogs.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-service-request-new-date',
  templateUrl: './service-request-new-date.component.html',
  styleUrls: [ './service-request-new-date.component.scss' ]
})
export class ServiceRequestNewDateComponent implements OnInit {
  public ObsStatus: typeof ObservableStatus = ObservableStatus;

  serviceRequest$: Observable<ServiceRequest>;

  updatingServiceRequest = false;

  constructor(
    private serviceRequestService: ServiceRequestService,
    private router: Router,
    private dateDialog: MatDialog,
    private dialogsService: DialogsService
  ) {
  }

  ngOnInit(): void {
    this.loadData();
  }

  private loadData(): void {
    const client: Client = JSON.parse(localStorage.getItem('current_person'));
    this.serviceRequest$ = this.serviceRequestService.getPendingServiceRequest(client.id);
  }

  suggestAnotherDate(serviceRequest: ServiceRequest): void {
    const dateRef = this.dateDialog.open(SelectDateModalComponent, {
      width: '700px'
    });

    dateRef.afterClosed().subscribe((date) => {
      if (date) {
        serviceRequest.date = date;
        this.acceptSuggestedDate(serviceRequest);
      }
    });
  }

  acceptSuggestedDate(serviceRequest: ServiceRequest): void {
    serviceRequest.state = ServiceRequestState.Pending;
    this.updateServiceRequest(serviceRequest);
  }

  private updateServiceRequest(serviceRequest: ServiceRequest): void {
    this.updatingServiceRequest = true;
    this.serviceRequestService.updateServiceRequest(serviceRequest).subscribe((serviceRequestUpdate) => {
      this.dialogsService.showSuccessDialog(
        'Fecha de la solicitud del servicio modificada con éxito. Espere nuestra respuesta.',
        () => {
          this.router.navigateByUrl('/user/profile');
        }
      );
    });
  }
}
