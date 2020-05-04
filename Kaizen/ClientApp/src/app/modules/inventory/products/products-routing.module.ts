import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ProductsComponent } from './components/products/products.component';
import { ProductRegisterComponent } from './components/product-register/product-register.component';
import { AuthGuard } from '@app/core/guards/auth.guard';
import { AdminGuard } from '@app/core/guards/admin.guard';

const routes: Routes = [
	{
		path: '',
		canActivate: [ AuthGuard, AdminGuard ],
		children: [
			{ path: '', component: ProductsComponent },
			{ path: 'register', component: ProductRegisterComponent }
		]
	}
];

@NgModule({
	imports: [ RouterModule.forChild(routes) ],
	exports: [ RouterModule ]
})
export class ProductsRoutingModule {}
