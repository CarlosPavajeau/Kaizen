import { Pipe, PipeTransform } from '@angular/core';
import { InvoiceState } from '@app/modules/payments/models/invoice-state';

@Pipe({
  name: 'invoiceState'
})
export class InvoiceStatePipe implements PipeTransform {
  transform(value: InvoiceState): string {
    switch (value) {
      case InvoiceState.Generated:
        return 'Generada';
      case InvoiceState.Regenerated:
        return 'ReGenerada';
      case InvoiceState.Paid:
        return 'Pagada';
      case InvoiceState.Expired:
        return 'Expirada';
      default:
        return 'Error: Estado de factura invalido';
        break;
    }
  }
}
