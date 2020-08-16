import { Component, OnInit, QueryList, ViewChildren } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSelect, MatSelectChange } from '@angular/material/select';
import { ActivatedRoute } from '@angular/router';
import { IForm } from '@core/models/form';
import { MonthBit, MONTHS } from '@core/models/months';
import { Product } from '@modules/inventory/products/models/product';
import { ProductService } from '@modules/inventory/products/services/product.service';
import { NotificationsService } from '@shared/services/notifications.service';

@Component({
  selector: 'app-product-edit',
  templateUrl: './product-edit.component.html',
  styleUrls: [ './product-edit.component.css' ]
})
export class ProductEditComponent implements OnInit, IForm {
  allMonths: MonthBit[];
  applicationMonths: number;
  @ViewChildren('monthSelect') monthSelect: QueryList<MatSelect>;

  product: Product;

  productForm: FormGroup;
  productInInventoryForm: FormGroup;
  productDocumentsForm: FormGroup;

  get controls(): { [key: string]: AbstractControl } {
    return this.productForm.controls;
  }

  get productInInventoryControls(): { [key: string]: AbstractControl } {
    return this.productInInventoryForm.controls;
  }

  get productDocumentsControls(): { [key: string]: AbstractControl } {
    return this.productDocumentsForm.controls;
  }

  constructor(
    private productService: ProductService,
    private activatedRoute: ActivatedRoute,
    private formBuilder: FormBuilder,
    private notificationsService: NotificationsService
  ) {}

  ngOnInit(): void {
    this.initForm();
    this.loadData();

    this.allMonths = MONTHS;
  }

  initForm(): void {
    this.initProductForm();
    this.initProductInInventoryForm();
  }

  private initProductForm(): void {
    this.productForm = this.formBuilder.group({
      name: [ '', [ Validators.required, Validators.maxLength(40) ] ],
      description: [ '', [ Validators.required, Validators.maxLength(300) ] ],
      applicationMonths: [ '', [ Validators.required ] ]
    });
  }

  private initProductInInventoryForm(): void {
    this.productInInventoryForm = this.formBuilder.group({
      amount: [ '', [ Validators.required ] ],
      presentation: [ '', [ Validators.required ] ],
      price: [ '', [ Validators.required ] ]
    });
  }

  private loadData(): void {
    const code = this.activatedRoute.snapshot.paramMap.get('code');
    this.productService.getProduct(code).subscribe((product) => {
      this.product = product;
      this.afterLoadProduct();
    });
  }

  private afterLoadProduct(): void {
    this.applicationMonths = this.product.applicationMonths;

    this.productForm.setValue({
      name: this.product.name,
      description: this.product.description,
      applicationMonths: 0
    });

    this.productInInventoryForm.setValue({
      amount: this.product.amount,
      presentation: this.product.presentation,
      price: this.product.price
    });

    setTimeout(() => {
      this.monthSelect.first.options.forEach((option) => {
        // tslint:disable-next-line: no-bitwise
        if (option.value & this.product.applicationMonths) {
          option.select();
        }
      });
    }, 0);
  }

  onSelectMonth(event: MatSelectChange): void {
    const option = event.source;
    const values = option.value as [];
    this.applicationMonths = 0;
    values.forEach((value) => {
      // tslint:disable-next-line: no-bitwise
      this.applicationMonths |= value;
    });
  }

  updateProductBasicData(): void {
    if (this.productForm.valid) {
      this.product.name = this.controls['name'].value;
      this.product.description = this.controls['description'].value;
      this.product.applicationMonths = this.applicationMonths;
      this.updateProduct();
    }
  }

  updateProductInInventory(): void {
    if (this.productInInventoryForm.valid) {
      this.product.amount = this.productInInventoryControls['amount'].value;
      this.product.presentation = this.productInInventoryControls['presentation'].value;
      this.product.price = this.productInInventoryControls['price'].value;
      this.updateProduct();
    }
  }

  private updateProduct(): void {
    this.productService.updateProduct(this.product).subscribe((productUpdated) => {
      if (productUpdated) {
        this.notificationsService.showSuccessMessage(
          `Datos básicos del producto ${this.product.name} actualizados.`,
          () => {
            this.product = productUpdated;
            this.afterLoadProduct();
          }
        );
      }
    });
  }
}
