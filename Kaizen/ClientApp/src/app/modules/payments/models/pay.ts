export interface PayModel {
  description: string;
  transactionAmount: number;
  cardNumber: string;
  cardholderName: string;
  cardExpirationMonth?: string;
  cardExpirationYear?: string;
  securityCode: string;
  installments: number;
  docType: string;
  docNumber: string;
  email: string;
  paymentMethodId?: string;
  token?: string;
}
