import { Client } from '@app/modules/clients/models/client';
import { ServiceInvoiceDetail } from './service-invoice-detail';
import { PaymentMethod } from './payment-method';
import { InvoiceState } from './invoice-state';
import { Invoice } from './invoice';

export interface ServiceInvoice extends Invoice {
  serviceInvoiceDetails?: ServiceInvoiceDetail[];
}
