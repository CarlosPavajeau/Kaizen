import { HttpClient } from '@angular/common/http';
import { EventEmitter, Injectable } from '@angular/core';
import { environment } from '@base/environments/environment';
import { PayModel } from '@modules/payments/models/pay';
import { Observable } from 'rxjs';
import { ProductInvoice } from "@modules/payments/models/product-invoice";
import { PAYMENTS_API_URL } from "@global/endpoints";
import { ServiceInvoice } from "@modules/payments/models/service-invoice";

declare const Mercadopago: any;
const cardToken = new EventEmitter<string>();
const paymentMethod = new EventEmitter<string>();

@Injectable({
  providedIn: 'root'
})
export class PaymentService {
  constructor(private http: HttpClient) {
    Mercadopago.setPublishableKey(environment.mercadoPagoPublicKey);
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
      this.onGetPaymentMethod
    );
    return paymentMethod.asObservable();
  }

  private onGetPaymentMethod(status: number, response: any): void {
    if (status === 200) {
      const paymentMethodId = response[0].id;
      paymentMethod.emit(paymentMethodId);
    } else {
      paymentMethod.emit(null);
    }
  }

  payProductInvoice(productInvoice: ProductInvoice, paymentModel: PayModel): Observable<ProductInvoice> {
    return this.http.post<ProductInvoice>(`${ PAYMENTS_API_URL }/PayProductInvoice/${ productInvoice.id }`, paymentModel);
  }

  payServiceInvoice(serviceInvoice: ServiceInvoice, paymentModel: PayModel): Observable<ServiceInvoice> {
    return this.http.post<ServiceInvoice>(`${ PAYMENTS_API_URL }/PayServiceInvoice/${ serviceInvoice.id }`, paymentModel);
  }
}
