import { Directive } from '@angular/core';
import { NG_ASYNC_VALIDATORS, AsyncValidator, AbstractControl, ValidationErrors } from '@angular/forms';
import { Observable } from 'rxjs';
import { CheckEmployeeExistsService } from '@app/core/services/check-employee-exists.service';
import { map } from 'rxjs/operators';

@Directive({
	selector: '[uniqueEmployee]',
	providers: [ { provide: NG_ASYNC_VALIDATORS, useExisting: UniqueEmployeeDirective, multi: true } ]
})
export class UniqueEmployeeDirective implements AsyncValidator {
	constructor(private checkEmployeeExists: CheckEmployeeExistsService) {}

	validate(control: AbstractControl): Promise<ValidationErrors> | Observable<ValidationErrors> {
		const id = control.value;
		return this.checkEmployeeExists.checkEmployeeExists(id).pipe(
			map((result) => {
				if (result) {
					return { employeeExists: true };
				}
				return null;
			})
		);
	}
}
