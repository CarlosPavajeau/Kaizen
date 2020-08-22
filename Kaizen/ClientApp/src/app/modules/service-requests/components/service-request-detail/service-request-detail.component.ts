import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { ServiceRequest } from '@modules/service-requests/models/service-request';
import { ServiceRequestService } from '@modules/service-requests/services/service-request.service';

@Component({
  selector: 'app-service-request-detail',
  templateUrl: './service-request-detail.component.html',
  styleUrls: [ './service-request-detail.component.css' ]
})
export class ServiceRequestDetailComponent implements OnInit {
  serviceRequest: ServiceRequest;

  @Input() serviceRequestCode: number;
  @Output() serviceRequestLoaded = new EventEmitter<ServiceRequest>();

  constructor(
    private serviceRequestService: ServiceRequestService,
    private activateRoute: ActivatedRoute,
    public dateDialog: MatDialog
  ) {}

  ngOnInit(): void {
    const code =
      this.serviceRequestCode ? this.serviceRequestCode :
      +this.activateRoute.snapshot.paramMap.get('code');

    this.serviceRequestService.getServiceRequest(code).subscribe((serviceRequest) => {
      this.serviceRequest = serviceRequest;
      this.serviceRequest.date = new Date(this.serviceRequest.date);
      if (this.serviceRequestCode) {
        this.serviceRequestLoaded.emit(this.serviceRequest);
      }
    });
  }
}
