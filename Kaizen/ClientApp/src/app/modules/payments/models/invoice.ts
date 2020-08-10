import { InvoiceState } from './invoice-state';
import { PaymentMethod } from './payment-method';
import { Client } from '@modules/clients/models/client';

export interface Invoice {
  id: number;
  state: InvoiceState;
  paymentMethod: PaymentMethod;

  client?: Client;

  iva: number;
  subTotal: number;
  total: number;
}
