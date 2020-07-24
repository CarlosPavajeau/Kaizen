import { Pipe, PipeTransform } from '@angular/core';
import { Employee } from '@modules/employees/models/employee';

@Pipe({
	name: 'filterEmployees'
})
export class FilterEmployeesPipe implements PipeTransform {
	transform(value: Employee[], filterText: string): Employee[] {
		if (filterText === '' || filterText === undefined || filterText === null) {
			return value;
		}

		return value.filter((employee) => {
			const full_name = `${employee.firstName}${employee.secondName}${employee.lastName}${employee.secondLastName}`
				.trim()
				.toLowerCase();

			return employee.id.indexOf(filterText) !== -1 || full_name.indexOf(filterText.trim().toLowerCase()) !== -1;
		});
	}
}
