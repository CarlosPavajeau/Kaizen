import { NgModule } from '@angular/core';

import { ProductsRoutingModule } from './products-routing.module';
import { ProductRegisterComponent } from './components/product-register/product-register.component';
import { ProductsComponent } from './components/products/products.component';
import { SharedModule } from '@app/shared/shared.module';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';

@NgModule({
	declarations: [ ProductRegisterComponent, ProductsComponent ],
	imports: [ SharedModule, ProductsRoutingModule, ReactiveFormsModule, FormsModule ]
})
export class ProductsModule {}
