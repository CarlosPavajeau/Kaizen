import { Injectable } from '@angular/core';
import { Employee } from '@modules/employees/models/employee';
import { EmployeeLocation } from '@modules/employees/models/employee-location';
import { EmployeeSignalrService } from '@modules/employees/services/employee-signalr.service';

@Injectable({
  providedIn: 'root'
})
export class EmployeeLocationService {

  private employeeSendLocationInterval: NodeJS.Timeout;
  private currentEmployee: Employee;

  constructor(private employeeSignalr: EmployeeSignalrService) {
  }

  public startToSendEmployeeLocation(): void {
    this.employeeSendLocationInterval = setInterval(() => {
      if (!this.currentEmployee) {
        this.currentEmployee = JSON.parse(localStorage.getItem('current_person'));
      }

      if (!this.currentEmployee) {
        return;
      }

      if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition((position => {
          if (position) {
            const employeeLocation: EmployeeLocation = {
              id: this.currentEmployee.id,
              employee: this.currentEmployee,
              location: {
                latitude: position.coords.latitude,
                longitude: position.coords.longitude
              }
            };

            this.employeeSignalr.sendNewEmployeeLocation(employeeLocation);
          }
        }));
      }
    }, 5000);
  }

  public endToSendEmployeeLocation(): void {
    if (this.employeeSendLocationInterval) {
      clearInterval(this.employeeSendLocationInterval);
    }
  }
}
