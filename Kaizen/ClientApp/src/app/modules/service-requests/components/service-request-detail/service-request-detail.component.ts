import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { ServiceRequest } from '@modules/service-requests/models/service-request';
import { ServiceRequestService } from '@modules/service-requests/services/service-request.service';
import { ObservableStatus } from '@shared/models/observable-with-status';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-service-request-detail',
  templateUrl: './service-request-detail.component.html'
})
export class ServiceRequestDetailComponent implements OnInit {
  public ObsStatus: typeof ObservableStatus = ObservableStatus;

  serviceRequest$: Observable<ServiceRequest>;
  serviceRequest: ServiceRequest;

  @Input() serviceRequestCode: number;
  @Output() serviceRequestLoaded = new EventEmitter<ServiceRequest>();

  constructor(
    private serviceRequestService: ServiceRequestService,
    private activateRoute: ActivatedRoute,
    public dateDialog: MatDialog
  ) {
  }

  ngOnInit(): void {
    const code =
      this.serviceRequestCode ? this.serviceRequestCode :
        +this.activateRoute.snapshot.paramMap.get('code');

    this.serviceRequest$ = this.serviceRequestService.getServiceRequest(code);
    this.serviceRequest$.subscribe((serviceRequest) => {
      this.serviceRequest = serviceRequest;
      this.serviceRequest.date = new Date(this.serviceRequest.date);
      if (this.serviceRequestCode) {
        this.serviceRequestLoaded.emit(this.serviceRequest);
      }
    });
  }
}
