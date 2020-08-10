import { Component, OnInit } from '@angular/core';
import { ServiceRequestState } from '@app/modules/service-requests/models/service-request-state';
import { ServiceRequest } from '@modules/service-requests/models/service-request';
import { NewServiceRequestSignalrService } from '@modules/service-requests/services/new-service-request-signalr.service';
import { ServiceRequestService } from '@modules/service-requests/services/service-request.service';
import { NotificationsService } from '@shared/services/notifications.service';

@Component({
  selector: 'app-service-requests',
  templateUrl: './service-requests.component.html',
  styleUrls: [ './service-requests.component.css' ]
})
export class ServiceRequestsComponent implements OnInit {
  serviceRequests: ServiceRequest[] = [];

  constructor(
    private serviceRequestService: ServiceRequestService,
    private newServiceRequestSignalR: NewServiceRequestSignalrService,
    private notificationsService: NotificationsService
  ) {}

  ngOnInit(): void {
    this.loadServiceRequests();
    this.newServiceRequestSignalR.signalReceived.subscribe((data: ServiceRequest) => {
      if (data) {
        this.notificationsService.addMessage(`Se ha hecho una nueva solicitud de servicio`, 'Ok', 'left');
        this.serviceRequests.push(data);
      }
    });
  }

  private loadServiceRequests(): void {
    this.serviceRequestService.getServiceRequests().subscribe((serviceRequests) => {
      this.serviceRequests = serviceRequests;
    });
  }

  rejectServiceRequest(serviceRequest: ServiceRequest): void {
    serviceRequest.state = ServiceRequestState.Rejected;
    this.serviceRequestService.updateServiceRequest(serviceRequest).subscribe((serviceRequestUpdate) => {
      if (serviceRequestUpdate) {
        this.serviceRequests = this.serviceRequests.filter((s) => s.code !== serviceRequestUpdate.code);
      }
    });
  }
}
