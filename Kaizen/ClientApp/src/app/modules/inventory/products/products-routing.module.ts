import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ProductsComponent } from './components/products/products.component';
import { ProductRegisterComponent } from './components/product-register/product-register.component';
import { AuthGuard } from '@core/guards/auth.guard';
import { AdminGuard } from '@core/guards/admin.guard';
import { ProductDetailComponent } from './components/product-detail/product-detail.component';
import { ProductEditComponent } from './components/product-edit/product-edit.component';

const routes: Routes = [
  {
    path: '',
    canActivate: [ AuthGuard, AdminGuard ],
    children: [
      {
        path: '',
        component: ProductsComponent
      },
      {
        path: 'register',
        component: ProductRegisterComponent
      },
      {
        path: 'edit/:code',
        component: ProductEditComponent
      },
      {
        path: ':code',
        component: ProductDetailComponent
      }
    ]
  }
];

@NgModule({
  imports: [ RouterModule.forChild(routes) ],
  exports: [ RouterModule ]
})
export class ProductsRoutingModule {}
