import { NgModule } from '@angular/core';

import { ProductsRoutingModule } from './products-routing.module';
import { ProductRegisterComponent } from './components/product-register/product-register.component';
import { ProductsComponent } from './components/products/products.component';
import { SharedModule } from '@app/shared/shared.module';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { ProductDetailComponent } from './components/product-detail/product-detail.component';
import { ProductEditComponent } from './components/product-edit/product-edit.component';

@NgModule({
  declarations: [ ProductRegisterComponent, ProductsComponent, ProductDetailComponent, ProductEditComponent ],
  imports: [ SharedModule, ProductsRoutingModule, ReactiveFormsModule, FormsModule ]
})
export class ProductsModule {}
