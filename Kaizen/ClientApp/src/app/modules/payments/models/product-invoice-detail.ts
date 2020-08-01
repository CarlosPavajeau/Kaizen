import { Invoice } from './invoice';
import { Product } from '@app/modules/inventory/products/models/product';

export interface ProductInvoiceDetail {
	id: number;
	productCode: string;
	productName: string;
	amount: number;

	detail?: Product;

	total: number;
}
