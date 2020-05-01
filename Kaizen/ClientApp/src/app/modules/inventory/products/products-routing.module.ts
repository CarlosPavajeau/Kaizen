import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ProductsComponent } from './components/products/products.component';
import { ProductRegisterComponent } from './components/product-register/product-register.component';

const routes: Routes = [
	{ path: '', component: ProductsComponent },
	{ path: 'register', component: ProductRegisterComponent }
];

@NgModule({
	imports: [ RouterModule.forChild(routes) ],
	exports: [ RouterModule ]
})
export class ProductsRoutingModule {}
