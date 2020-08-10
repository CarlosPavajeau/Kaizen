import { ProductInvoiceDetail } from './product-invoice-detail';
import { Invoice } from './invoice';

export interface ProductInvoice extends Invoice {
  productInvoiceDetails: ProductInvoiceDetail[];
}
