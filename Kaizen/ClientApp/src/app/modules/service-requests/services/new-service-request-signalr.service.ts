import { AuthenticationService } from '@app/core/authentication/authentication.service';
import { BaseSignalrService } from '@app/core/services/base-signalr.service';
import { Injectable } from '@angular/core';
import { ServiceRequest } from '../models/service-request';

@Injectable({
  providedIn: 'root'
})
export class NewServiceRequestSignalrService extends BaseSignalrService<ServiceRequest> {
  constructor(authService: AuthenticationService) {
    super(authService, '/ServiceRequestsHub', 'NewServiceRequest');
  }
}
