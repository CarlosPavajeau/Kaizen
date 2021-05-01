import { Component, OnInit } from '@angular/core';
import { ServiceRequestState } from '@modules/service-requests/models/service-request-state';
import { ServiceRequest } from '@modules/service-requests/models/service-request';
import { ServiceRequestSignalrService } from '@modules/service-requests/services/service-request-signalr.service';
import { ServiceRequestService } from '@modules/service-requests/services/service-request.service';
import { ObservableStatus } from '@shared/models/observable-with-status';
import { SnackBarService } from '@shared/services/snack-bar.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-service-requests',
  templateUrl: './service-requests.component.html'
})
export class ServiceRequestsComponent implements OnInit {
  public ObsStatus: typeof ObservableStatus = ObservableStatus;

  serviceRequests: ServiceRequest[] = [];
  serviceRequests$: Observable<ServiceRequest[]>;

  constructor(
    private serviceRequestService: ServiceRequestService,
    private serviceRequestSignalrService: ServiceRequestSignalrService,
    private snackBarService: SnackBarService
  ) {
  }

  ngOnInit(): void {
    this.loadServiceRequests();

    this.serviceRequestSignalrService.onNewServiceRequestRegister.subscribe((data: ServiceRequest) => {
      if (data) {
        this.snackBarService.addMessage(`Se ha hecho una nueva solicitud de servicio`, 'Ok', 'left');
        this.serviceRequests.push(data);
      }
    });
  }

  private loadServiceRequests(): void {
    this.serviceRequests$ = this.serviceRequestService.getServiceRequests();
    this.serviceRequests$.subscribe((serviceRequests) => {
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
