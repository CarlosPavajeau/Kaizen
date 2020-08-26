import { HttpClient } from '@angular/common/http';
import { EventEmitter, Injectable } from '@angular/core';
import { PayModel } from '@modules/payments/models/pay';
import { Observable } from 'rxjs';

declare const Mercadopago: any;
const cardToken = new EventEmitter<string>();
const paymentMethod = new EventEmitter<string>();

@Injectable({
  providedIn: 'root'
})
export class PaymentService {
  constructor(private http: HttpClient) {
    Mercadopago.setPublishableKey('PUBLIC_KEY');
  }

  tokenizeCard(payModel: PayModel): Observable<string> {
    Mercadopago.createToken(payModel, this.sdkResponseHandler);
    return cardToken.asObservable();
  }

  private sdkResponseHandler(status: number, response: any): void {
    if (status !== 200 && status !== 201) {
      cardToken.emit(null);
    } else {
      const token = response.id;
      cardToken.emit(token);
    }
  }

  getPaymentMethod(cardNumber: string): Observable<string> {
    const bin = cardNumber.substring(0, 6);
    Mercadopago.getPaymentMethod(
      {
        bin: bin
      },
      this.onGetPaymentMehod
    );
    return paymentMethod.asObservable();
  }

  private onGetPaymentMehod(status: number, response: any): void {
    if (status === 200) {
      const paymentMethodId = response[0].id;
      paymentMethod.emit(paymentMethodId);
    } else {
      paymentMethod.emit(null);
    }
  }
}
