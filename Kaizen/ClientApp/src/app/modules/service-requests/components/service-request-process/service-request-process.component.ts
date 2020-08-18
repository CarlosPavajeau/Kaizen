import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { Employee } from '@modules/employees/models/employee';
import { EmployeeService } from '@modules/employees/services/employee.service';
import { ServiceRequest } from '@modules/service-requests/models/service-request';
import { ServiceRequestState } from '@modules/service-requests/models/service-request-state';
import { ServiceRequestService } from '@modules/service-requests/services/service-request.service';
import { SelectDateModalComponent } from '@shared/components/select-date-modal/select-date-modal.component';
import { NotificationsService } from '@shared/services/notifications.service';

@Component({
  selector: 'app-service-request-process',
  templateUrl: './service-request-process.component.html',
  styleUrls: [ './service-request-process.component.css' ]
})
export class ServiceRequestProcessComponent implements OnInit {
  serviceRequest: ServiceRequest;
  techniciansAvailable: Employee[] = [];
  serviceRequestCode: number;

  updatingServiceRequest = false;

  constructor(
    private serviceRequestService: ServiceRequestService,
    private employeeService: EmployeeService,
    private activateRoute: ActivatedRoute,
    private router: Router,
    public dateDialog: MatDialog,
    private notificationsService: NotificationsService
  ) {}

  ngOnInit(): void {
    this.loadData();
  }

  private loadData(): void {
    const code = +this.activateRoute.snapshot.paramMap.get('code');
    this.serviceRequestCode = code;
  }

  onLoadedServiceRequest(serviceRequest: ServiceRequest): void {
    this.serviceRequest = serviceRequest;
    const serviceCodes = serviceRequest.services.map((service) => service.code);
    this.employeeService
      .getTechniciansAvailable(serviceRequest.date, serviceCodes)
      .subscribe((techniciansAvailable) => {
        this.techniciansAvailable = techniciansAvailable;
      });
  }

  cancelServiceRequest(): void {
    this.serviceRequest.state = ServiceRequestState.Rejected;
    this.updatingServiceRequest = true;
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
        this.updatingServiceRequest = true;
        this.serviceRequestService.updateServiceRequest(this.serviceRequest).subscribe((serviceRequestUpdate) => {
          if (serviceRequestUpdate) {
            this.notificationsService.showSuccessMessage(`Fecha de solicitud de servicio modificada con éxito`, () => {
              this.router.navigateByUrl('/service_requests');
            });
          }
        });
      }
    });
  }
}
