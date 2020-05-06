import { Observable } from 'rxjs';
import { AbstractControl, ValidationErrors, AsyncValidator } from '@angular/forms';
import { Injectable } from '@angular/core';
import { CheckEquipmentExistsService } from '@core/services/check-equipment-exists.service';
import { UniqueEquipmentDirective } from '@shared/directives/unique-equipment.directive';

@Injectable({
	providedIn: 'root'
})
export class EquipmentExistsValidator implements AsyncValidator {
	constructor(private checkEquipmentService: CheckEquipmentExistsService) {}

	validate(control: AbstractControl): Promise<ValidationErrors> | Observable<ValidationErrors> {
		const uniqueEquipmentDirective = new UniqueEquipmentDirective(this.checkEquipmentService);
		return uniqueEquipmentDirective.validate(control);
	}
}
