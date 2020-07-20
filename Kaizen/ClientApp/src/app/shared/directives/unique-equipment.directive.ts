import { Directive } from '@angular/core';
import { NG_ASYNC_VALIDATORS, AsyncValidator, AbstractControl, ValidationErrors } from '@angular/forms';
import { Observable } from 'rxjs';
import { CheckEquipmentExistsService } from '@core/services/check-equipment-exists.service';
import { map } from 'rxjs/operators';

@Directive({
	selector: '[appUniqueEquipment]',
	providers: [ { provide: NG_ASYNC_VALIDATORS, useExisting: UniqueEquipmentDirective, multi: true } ]
})
export class UniqueEquipmentDirective implements AsyncValidator {
	constructor(private checkEquipmentExistsService: CheckEquipmentExistsService) {}

	validate(control: AbstractControl): Promise<ValidationErrors> | Observable<ValidationErrors> {
		const id = control.value;
		return this.checkEquipmentExistsService.checkEntityExists(id).pipe(
			map((result) => {
				if (result) {
					return { equipmentExists: true };
				}
				return null;
			})
		);
	}
}
