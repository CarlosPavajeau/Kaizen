import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { Client } from '@modules/clients/models/client';
import { ServiceRequest } from '@modules/service-requests/models/service-request';
import { ServiceRequestState } from '@modules/service-requests/models/service-request-state';
import { ServiceRequestService } from '@modules/service-requests/services/service-request.service';
import { SelectDateModalComponent } from '@shared/components/select-date-modal/select-date-modal.component';
import { NotificationsService } from '@shared/services/notifications.service';

@Component({
  selector: 'app-service-request-new-date',
  templateUrl: './service-request-new-date.component.html',
  styleUrls: [ './service-request-new-date.component.css' ]
})
export class ServiceRequestNewDateComponent implements OnInit {
  serviceRequest: ServiceRequest;

  constructor(
    private serviceRequestService: ServiceRequestService,
    private router: Router,
    private dateDialog: MatDialog,
    private notificationsService: NotificationsService
  ) {}

  ngOnInit(): void {
    this.loadData();
  }

  private loadData(): void {
    const client: Client = JSON.parse(localStorage.getItem('current_person'));

    this.serviceRequestService.getPendingServiceRequest(client.id).subscribe((pendingRequest) => {
      this.serviceRequest = pendingRequest;
    });
  }

  suggestAnotherDate(): void {
    const dateRef = this.dateDialog.open(SelectDateModalComponent, {
      width: '700px'
    });

    dateRef.afterClosed().subscribe((date) => {
      if (date) {
        this.serviceRequest.date = date;
        this.acceptSuggestedDate();
      }
    });
  }

  acceptSuggestedDate(): void {
    this.serviceRequest.state = ServiceRequestState.Pending;
    this.updateServiceRequest();
  }

  private updateServiceRequest(): void {
    this.serviceRequestService.updateServiceRequest(this.serviceRequest).subscribe((serviceRequestUpdate) => {
      this.notificationsService.showSuccessMessage(
        'Fecha de la solicitud del servicio modificada con éxito. Espere nuestra respuesta.',
        () => {
          this.router.navigateByUrl('/user/profile');
        }
      );
    });
  }
}
