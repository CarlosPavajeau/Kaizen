import { EventEmitter, Injectable } from '@angular/core';
import { AuthenticationService } from '@core/authentication/authentication.service';
import { BaseSignalrService } from '@core/services/base-signalr.service';
import { EmployeeLocation } from '@modules/employees/models/employee-location';

@Injectable({
  providedIn: 'root'
})
export class EmployeeSignalrService extends BaseSignalrService {
  onUpdateEmployeeLocation: EventEmitter<EmployeeLocation> = new EventEmitter<EmployeeLocation>();

  constructor(private authService: AuthenticationService) {
    super();
    this.startConnection();
    this.addOnUpdateEmployeeLocation();
  }

  public startConnection(): void {
    super.buildConnection('/EmployeeLocation', this.authService.getToken());
    super.startConnection();
  }

  public addOnUpdateEmployeeLocation(): void {
    this.hubConnection.on('OnUpdateEmployeeLocation', (employeeLocation: EmployeeLocation) => {
      this.onUpdateEmployeeLocation.emit(employeeLocation);
    });
  }

  public sendNewEmployeeLocation(employeeLocation: EmployeeLocation): void {
    this.hubConnection.invoke('UpdateEmployeeLocation', employeeLocation)
      .then(() => {
      })
      .catch(err => {
        console.log('Error: ' + err);
      });
  }
}
