import { Pipe, PipeTransform } from '@angular/core';
import { Equipment } from '@modules/inventory/equipments/models/equipment';

@Pipe({
	name: 'filterEquipments'
})
export class FilterEquipmentsPipe implements PipeTransform {
	transform(value: Equipment[], filterText: string): Equipment[] {
		if (filterText === '' || filterText === undefined || filterText === null) {
			return value;
		}

		filterText = filterText.trim().toLowerCase();
		return value.filter((equipment) => {
			return (
				equipment.code.toLowerCase().indexOf(filterText) !== -1 ||
				equipment.name.toLowerCase().indexOf(filterText)
			);
		});
	}
}
