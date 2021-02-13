import { EventEmitter, Injectable } from '@angular/core';
import { AuthenticationService } from '@core/authentication/authentication.service';
import { BaseSignalrService } from '@core/services/base-signalr.service';
import { ServiceRequest } from '@modules/service-requests/models/service-request';

@Injectable({
  providedIn: 'root'
})
export class ServiceRequestSignalrService extends BaseSignalrService {
  onNewServiceRequestRegister: EventEmitter<ServiceRequest>;

  constructor(private authService: AuthenticationService) {
    super();
  }

  public startConnection() {
    super.buildConnection('/ServiceRequestHub', this.authService.getToken());
    super.startConnection();
  }

  public addOnNewServiceRequestRegister(): void {
    this.hubConnection.on('NewServiceRequest', (serviceRequest: ServiceRequest) => {
      this.onNewServiceRequestRegister.emit(serviceRequest);
    });
  }
}
