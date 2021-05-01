import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { IForm } from '@core/models/form';
import { buildIsoDate } from '@core/utils/date-utils';
import { zeroPad } from '@core/utils/number-utils';
import { Client } from '@modules/clients/models/client';
import { Product } from '@modules/inventory/products/models/product';
import { ProductService } from '@modules/inventory/products/services/product.service';
import { InvoiceState } from '@modules/payments/models/invoice-state';
import { PaymentMethod } from '@modules/payments/models/payment-method';
import { ProductInvoice } from '@modules/payments/models/product-invoice';
import { ProductInvoiceDetail } from '@modules/payments/models/product-invoice-detail';
import { ProductInvoiceService } from '@modules/payments/services/product-invoice.service';
import { ObservableStatus } from '@shared/models/observable-with-status';
import { DialogsService } from '@shared/services/dialogs.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-product-invoice-register',
  templateUrl: './product-invoice-register.component.html'
})
export class ProductInvoiceRegisterComponent implements OnInit, IForm {
  public ObsStatus: typeof ObservableStatus = ObservableStatus;

  products$: Observable<Product[]>;
  selectedProducts: ProductInvoiceDetail[] = [];
  subTotal = 0.0;

  productInvoiceForm: FormGroup;
  savingProductInvoice = false;

  get controls(): { [p: string]: AbstractControl } {
    return this.productInvoiceForm.controls;
  }

  constructor(
    private productService: ProductService,
    private productInvoiceService: ProductInvoiceService,
    private dialogsService: DialogsService,
    private formBuilder: FormBuilder,
    private router: Router
  ) {
  }

  ngOnInit(): void {
    this.initForm();
    this.loadData();
  }

  private loadData(): void {
    this.products$ = this.productService.getProducts();
  }

  initForm(): void {
    this.productInvoiceForm = this.formBuilder.group({
      product: [ '', [ Validators.required ] ],
      amount: [ '', [ Validators.required, Validators.min(1) ] ]
    });
  }

  onSubmit(): void {
    if (this.selectedProducts.length > 0) {
      const client: Client = JSON.parse(localStorage.getItem('current_person'));
      const today = new Date();
      const todayISO = buildIsoDate(today, `${ zeroPad(today.getHours(), 2) }:${ zeroPad(today.getMinutes(), 2) }`);
      const productInvoice: ProductInvoice = {
        state: InvoiceState.Generated,
        paymentMethod: PaymentMethod.None,
        generationDate: todayISO,
        clientId: client.id,
        productInvoiceDetails: this.selectedProducts
      };

      this.savingProductInvoice = true;
      this.productInvoiceService.saveProductInvoice(productInvoice).subscribe((result) => {
        if (result) {
          this.dialogsService.showSuccessDialog(
            'Factura de producto generada con éxito, puede proceder a pagarla.',
            () => {
              this.router.navigateByUrl(`payments/pay/product_invoice/${ result.id }`);
            }
          );
        }
      });
    }
  }

  addProduct(): void {
    if (this.productInvoiceForm.valid) {
      const productDetail: ProductInvoiceDetail = {
        productCode: this.controls['product'].value.code,
        detail: this.controls['product'].value,
        amount: +this.controls['amount'].value
      };
      productDetail.total = productDetail.amount * productDetail.detail.price;

      const productDetailFind = this.selectedProducts.find((p) => p.detail.code === productDetail.detail.code);
      if (!productDetailFind) {
        this.selectedProducts.push(productDetail);
      } else {
        productDetailFind.amount += productDetail.amount;
        productDetailFind.total += productDetail.total;
      }

      this.subTotal += productDetail.total;
      this.productInvoiceForm.reset();
    }
  }

  deleteProduct(productDetail: ProductInvoiceDetail): void {
    this.selectedProducts = this.selectedProducts.filter((p) => p.detail.code !== productDetail.detail.code);
    this.subTotal -= productDetail.total;
  }
}
