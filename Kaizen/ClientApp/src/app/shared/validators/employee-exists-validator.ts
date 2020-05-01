import { Observable } from 'rxjs';
import { AbstractControl, ValidationErrors, AsyncValidator } from '@angular/forms';
import { Injectable } from '@angular/core';
import { CheckEmployeeExistsService } from '@core/services/check-employee-exists.service';
import { UniqueEmployeeDirective } from '@shared/directives/unique-employee.directive';

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
