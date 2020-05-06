import { Directive } from '@angular/core';
import { NG_ASYNC_VALIDATORS, AsyncValidator, AbstractControl, ValidationErrors } from '@angular/forms';
import { CheckUserExistsService } from '@core/services/check-user-exists.service';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';

@Directive({
	selector: '[uniqueUser]',
	providers: [ { provide: NG_ASYNC_VALIDATORS, useExisting: UniqueUserDirective, multi: true } ]
})
export class UniqueUserDirective implements AsyncValidator {
	constructor(private checkUserService: CheckUserExistsService) {}

	validate(control: AbstractControl): Promise<ValidationErrors> | Observable<ValidationErrors> {
		const username = control.value;
		return this.checkUserService.checkEntityExists(username).pipe(
			map((result) => {
				if (result) {
					return { userExists: true };
				}
				return null;
			})
		);
	}
}
