import { Pipe, PipeTransform } from '@angular/core';
import { Product } from '@modules/inventory/products/models/product';

@Pipe({
	name: 'filterProducts'
})
export class FilterProductsPipe implements PipeTransform {
	transform(value: Product[], filterText: string): Product[] {
		if (filterText === '' || filterText === undefined || filterText === null) {
			return value;
		}

		filterText = filterText.trim().toLowerCase();
		return value.filter((product) => {
			return (
				product.code.toLowerCase().indexOf(filterText) !== -1 ||
				product.name.toLowerCase().indexOf(filterText) !== -1
			);
		});
	}
}
