import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProductsRoutingModule } from './products-routing.module';
import { ProductRegisterComponent } from './components/product-register/product-register.component';
import { ProductsComponent } from './components/products/products.component';


@NgModule({
  declarations: [ProductRegisterComponent, ProductsComponent],
  imports: [
    CommonModule,
    ProductsRoutingModule
  ]
})
export class ProductsModule { }
