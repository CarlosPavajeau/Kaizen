import { Invoice } from './invoice';
import { ProductInvoiceDetail } from './product-invoice-detail';

export interface ProductInvoice extends Invoice {
  clientId: string;
  productInvoiceDetails: ProductInvoiceDetail[];
}
