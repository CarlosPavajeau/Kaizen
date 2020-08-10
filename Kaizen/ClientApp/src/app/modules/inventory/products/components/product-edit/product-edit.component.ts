import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ProductService } from '@modules/inventory/products/services/product.service';
import { Product } from '@modules/inventory/products/models/product';
import { IForm } from '@core/models/form';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MonthBit, MONTHS } from '@core/models/months';
import { Observable } from 'rxjs';
import { MatAutocomplete, MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';
import { ENTER, COMMA } from '@angular/cdk/keycodes';
import { MatChipInputEvent } from '@angular/material/chips';
import { startWith, map } from 'rxjs/operators';
import { NotificationsService } from '@shared/services/notifications.service';

@Component({
  selector: 'app-product-edit',
  templateUrl: './product-edit.component.html',
  styleUrls: [ './product-edit.component.css' ]
})
export class ProductEditComponent implements OnInit, IForm {
  allMonths: MonthBit[];
  visible = true;
  selectable = true;
  removable = true;
  separatorKeysCodes: number[] = [ ENTER, COMMA ];
  filteredMonths: Observable<MonthBit[]>;
  applicationMonths: number;
  selectedMonths: MonthBit[] = [];
  @ViewChild('monthInput') monthInput: ElementRef<HTMLInputElement>;
  @ViewChild('auto') matAutocomplete: MatAutocomplete;

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
    this.filteredMonths = this.controls['applicationMonths'].valueChanges.pipe(
      startWith(<string>null),
      map(
        (month: string | null) =>

            month ? this._filter(month) :
            this.allMonths.slice()
      )
    );
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
    this.allMonths.forEach((month) => {
      if (month.value & this.product.applicationMonths) {
        this.selectedMonths.push(month);
        this.applicationMonths |= month.value;
        this.allMonths = this.allMonths.filter((m) => m.value !== month.value);
      }
    });
    this.controls['applicationMonths'].setValue(this.applicationMonths);
    this.controls['name'].setValue(this.product.name);
    this.controls['description'].setValue(this.product.description);

    this.productInInventoryControls['amount'].setValue(this.product.amount);
    this.productInInventoryControls['presentation'].setValue(this.product.presentation);
    this.productInInventoryControls['price'].setValue(this.product.price);
  }

  addMonth(event: MatChipInputEvent): void {
    const input = event.input;
    const value = event.value;

    const month = this.allMonths.find((m) => m.name === value);

    if (month) {
      this.applicationMonths |= +month.value;
      this.selectedMonths.push(month);
      this.allMonths = this.allMonths.filter((m) => m.value !== month.value);

      if (input) {
        input.value = '';
        this.controls['applicationMonths'].setValue(this.applicationMonths);
      }
    }
  }

  removeMonth(month: MonthBit) {
    this.applicationMonths -= month.value;
    this.selectedMonths = this.selectedMonths.filter((m) => m.value !== month.value);
    this.allMonths.push(month);
    this.controls['applicationMonths'].setValue(this.applicationMonths);
  }

  selectedMonth(event: MatAutocompleteSelectedEvent): void {
    const value = event.option.value;
    const viewValue = event.option.viewValue;
    this.applicationMonths |= +value.value;
    this.selectedMonths.push(value);
    this.allMonths = this.allMonths.filter((m) => m.value !== value.value);
    this.monthInput.nativeElement.value = '';
    this.controls['applicationMonths'].setValue(this.applicationMonths);
  }

  private _filter(value: string): MonthBit[] {
    if (typeof value === 'string') {
      const filterValue = value.toLowerCase();

      return this.allMonths.filter((month) => month.name.toLowerCase().indexOf(filterValue) === 0);
    } else {
      return this.allMonths.slice();
    }
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
