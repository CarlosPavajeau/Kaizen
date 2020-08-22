import { Injectable } from '@angular/core';
import { AbstractControl, AsyncValidator, ValidationErrors } from '@angular/forms';
import { CheckEmployeeExistsService } from '@core/services/check-employee-exists.service';
import { UniqueEmployeeDirective } from '@shared/directives/unique-employee.directive';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EmployeeExistsValidator implements AsyncValidator {
  constructor(private checkEmployeeService: CheckEmployeeExistsService) {}

  validate(control: AbstractControl): Promise<ValidationErrors> | Observable<ValidationErrors> {
    const uniqueEmployeeDirective = new UniqueEmployeeDirective(this.checkEmployeeService);
    return uniqueEmployeeDirective.validate(control);
  }
}
