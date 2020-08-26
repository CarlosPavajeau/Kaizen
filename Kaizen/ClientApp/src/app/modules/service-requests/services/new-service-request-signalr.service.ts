import { Injectable } from '@angular/core';
import { AuthenticationService } from '@core/authentication/authentication.service';
import { BaseSignalrService } from '@core/services/base-signalr.service';
import { ServiceRequest } from '@modules/service-requests/models/service-request';

@Injectable({
  providedIn: 'root'
})
export class NewServiceRequestSignalrService extends BaseSignalrService<ServiceRequest> {
  constructor(authService: AuthenticationService) {
    super(authService, '/ServiceRequestsHub', 'NewServiceRequest');
  }
}
