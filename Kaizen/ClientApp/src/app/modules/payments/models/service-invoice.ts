import { Invoice } from './invoice';
import { ServiceInvoiceDetail } from './service-invoice-detail';

export interface ServiceInvoice extends Invoice {
  serviceInvoiceDetails?: ServiceInvoiceDetail[];
}
