import { AfterViewInit, Component, OnInit, QueryList, ViewChildren } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSelect, MatSelectChange } from '@angular/material/select';
import { ActivatedRoute, Router } from '@angular/router';
import { IForm } from '@core/models/form';
import { MonthBit, MONTHS } from '@core/models/months';
import { Product } from '@modules/inventory/products/models/product';
import { ProductService } from '@modules/inventory/products/services/product.service';
import { ObservableStatus } from '@shared/models/observable-with-status';
import { NotificationsService } from '@shared/services/notifications.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-product-edit',
  templateUrl: './product-edit.component.html',
  styleUrls: [ './product-edit.component.scss' ]
})
export class ProductEditComponent implements OnInit, IForm, AfterViewInit {
  public ObsStatus: typeof ObservableStatus = ObservableStatus;

  allMonths: MonthBit[];
  applicationMonths: number;
  @ViewChildren('monthSelect') monthSelect: QueryList<MatSelect>;

  product$: Observable<Product>;

  productForm: FormGroup;
  productInInventoryForm: FormGroup;
  productDocumentsForm: FormGroup;

  uploadingProduct = false;

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
    private notificationsService: NotificationsService,
    private router: Router
  ) {}
  ngAfterViewInit(): void {
    console.log(this.monthSelect);
  }

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
    this.product$ = this.productService.getProduct(code);
    this.product$.subscribe((product) => {
      this.afterLoadProduct(product);
    });
  }

  private afterLoadProduct(product: Product): void {
    this.applicationMonths = product.applicationMonths;

    this.productForm.setValue({
      name: product.name,
      description: product.description,
      applicationMonths: 0
    });

    this.productInInventoryForm.setValue({
      amount: product.amount,
      presentation: product.presentation,
      price: product.price
    });

    setTimeout(() => {
      console.log(this.monthSelect);
      if (this.monthSelect.first) {
        this.monthSelect.first.options.forEach((option) => {
          // tslint:disable-next-line: no-bitwise
          if (option.value & product.applicationMonths) {
            option.select();
          }
        });
      }
    }, 100);
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

  updateProductBasicData(product: Product): void {
    if (this.productForm.valid) {
      product.name = this.controls['name'].value;
      product.description = this.controls['description'].value;
      product.applicationMonths = this.applicationMonths;
      this.updateProduct(product);
    }
  }

  updateProductInInventory(product: Product): void {
    if (this.productInInventoryForm.valid) {
      product.amount = this.productInInventoryControls['amount'].value;
      product.presentation = this.productInInventoryControls['presentation'].value;
      product.price = this.productInInventoryControls['price'].value;
      this.updateProduct(product);
    }
  }

  private updateProduct(product: Product): void {
    this.uploadingProduct = true;
    this.productService.updateProduct(product).subscribe((productUpdated) => {
      if (productUpdated) {
        this.notificationsService.showSuccessMessage(`Datos básicos del producto $product.name} actualizados.`, () => {
          this.router.navigateByUrl('/inventory/products');
          this.uploadingProduct = false;
        });
      }
    });
  }
}
