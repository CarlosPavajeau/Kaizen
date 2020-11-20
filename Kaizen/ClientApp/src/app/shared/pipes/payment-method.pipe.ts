import { Pipe, PipeTransform } from '@angular/core';
import { PaymentMethod } from '@base/app/modules/payments/models/payment-method';

@Pipe({
  name: 'paymentMethod'
})
export class PaymentMethodPipe implements PipeTransform {
  transform(value: PaymentMethod): string {
    switch (value) {
      case PaymentMethod.None:
        return 'Ninguno';
      case PaymentMethod.Cash:
        return 'Efectivo';
      case PaymentMethod.CreditCard:
        return 'Tarjeta de crédito';
      case PaymentMethod.BankDeposit:
        return 'Depósito bancario';
      default:
        return 'Método de pago inválido';
    }
  }
}
