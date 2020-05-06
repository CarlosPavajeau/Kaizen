import { Directive, Injectable } from '@angular/core';
import { NG_ASYNC_VALIDATORS, AsyncValidator, AbstractControl, ValidationErrors } from '@angular/forms';
import { Observable } from 'rxjs';
import { CheckClientExistsService } from '@core/services/check-client-exists.service';
import { map } from 'rxjs/operators';

@Directive({
	selector: '[uniqueClient]',
	providers: [ { provide: NG_ASYNC_VALIDATORS, useExisting: UniqueClientDirective, multi: true } ]
})
export class UniqueClientDirective implements AsyncValidator {
	constructor(private checkClientService: CheckClientExistsService) {}

	validate(control: AbstractControl): Promise<ValidationErrors> | Observable<ValidationErrors> {
		const id = control.value;
		return this.checkClientService.checkEntityExists(id).pipe(
			map((result) => {
				if (result) {
					return { clientExists: true };
				}
				return null;
			})
		);
	}
}
