import { Client } from '@modules/clients/models/client';
import { InvoiceState } from './invoice-state';
import { PaymentMethod } from './payment-method';

export interface Invoice {
  id: number;
  state: InvoiceState;
  paymentMethod: PaymentMethod;
  generationDate: Date;
  paymentDate: Date;

  client?: Client;

  iva: number;
  subTotal: number;
  total: number;
}
