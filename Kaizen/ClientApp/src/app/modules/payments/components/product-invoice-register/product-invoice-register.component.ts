import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IForm } from '@core/models/form';
import { Product } from '@modules/inventory/products/models/product';
import { ProductService } from '@modules/inventory/products/services/product.service';
import { ProductInvoiceDetail } from '@modules/payments/models/product-invoice-detail';
import { ProductInvoiceService } from '@modules/payments/services/product-invoice.service';
import { ObservableStatus } from '@shared/models/observable-with-status';
import { NotificationsService } from '@shared/services/notifications.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-product-invoice-register',
  templateUrl: './product-invoice-register.component.html',
  styleUrls: [ './product-invoice-register.component.scss' ]
})
export class ProductInvoiceRegisterComponent implements OnInit, IForm {
  public ObsStatus: typeof ObservableStatus = ObservableStatus;

  products$: Observable<Product[]>;
  selectedProducts: ProductInvoiceDetail[] = [];
  subTotal = 0.0;

  productInvoiceForm: FormGroup;

  get controls(): { [p: string]: AbstractControl } {
    return this.productInvoiceForm.controls;
  }

  constructor(
    private productService: ProductService,
    private productInvoiceService: ProductInvoiceService,
    private notificationsService: NotificationsService,
    private formBuilder: FormBuilder
  ) {}

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

  onSubmit(): void {}

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
