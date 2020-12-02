import { Component, OnInit, QueryList, ViewChildren } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSelectionList, MatSelectionListChange } from '@angular/material/list';
import { IForm } from '@core/models/form';
import { Product } from '@modules/inventory/products/models/product';
import { ProductService } from '@modules/inventory/products/services/product.service';
import { ObservableStatus } from '@shared/models/observable-with-status';
import { Observable } from 'rxjs';
import { delay } from 'rxjs/operators';

@Component({
  selector: 'app-select-products',
  templateUrl: './select-products.component.html',
  styleUrls: [ './select-products.component.scss' ]
})
export class SelectProductsComponent implements OnInit, IForm {
  public ObsStatus: typeof ObservableStatus = ObservableStatus;

  products$: Observable<Product[]>;

  selectedProducts: Product[] = [];

  selectProductsForm: FormGroup;
  @ViewChildren('productsListSelection') productsListSelection: QueryList<MatSelectionList>;

  get controls(): { [key: string]: AbstractControl } {
    return this.selectProductsForm.controls;
  }

  constructor(private productService: ProductService, private formBuilder: FormBuilder) {}

  ngOnInit(): void {
    this.initForm();
    this.loadData();
  }

  private loadData(): void {
    this.products$ = this.productService.getProducts();
  }

  initForm(): void {
    this.selectProductsForm = this.formBuilder.group({
      productCode: [ '' ],
      showSelectedProducts: [ false ],
      productCodes: [ '', [ Validators.required ] ]
    });

    this.controls['productCode'].valueChanges.pipe(delay(100)).subscribe((value) => {
      this.selectProducts();
    });

    this.controls['showSelectedProducts'].valueChanges.subscribe((value) => {
      if (value) {
        this.controls['productCode'].disable();
      } else {
        this.controls['productCode'].enable();
      }
    });
  }

  selectProducts(): void {
    if (
      this.selectedProducts.length === 0 ||
      this.productsListSelection.first === undefined ||
      this.productsListSelection.first.options === undefined
    ) {
      return;
    }

    const selectedOptions = this.productsListSelection.first.options.filter((option) => {
      return this.selectedProducts.indexOf(option.value) !== -1;
    });

    this.productsListSelection.first.selectedOptions.select(...selectedOptions);
  }

  onSelectProduct(event: MatSelectionListChange): void {
    const option = event.option;
    const value = option.value;
    if (option.selected) {
      this.selectedProducts.push(value);
    } else {
      const index = this.selectedProducts.indexOf(value);
      if (index !== -1) {
        this.selectedProducts.splice(index, 1);
      }
    }
  }

  get valid(): boolean {
    return this.selectProductsForm.valid;
  }

  get invalid(): boolean {
    return !this.valid;
  }

  get value(): any {
    if (this.selectedProducts.length === 0) {
      return null;
    }
    return this.controls['productCodes'].value.map((p: Product) => p.code);
  }

  setValue(value: Product[]): void {
    this.selectedProducts = value;
    this.selectProducts();
  }
}
