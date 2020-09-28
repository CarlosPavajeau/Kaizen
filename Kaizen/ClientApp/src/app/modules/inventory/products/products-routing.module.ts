import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardLayoutComponent } from '@app/shared/layouts/dashboard-layout/dashboard-layout.component';
import { AdminGuard } from '@core/guards/admin.guard';
import { AuthGuard } from '@core/guards/auth.guard';
import { ProductDetailComponent } from './components/product-detail/product-detail.component';
import { ProductEditComponent } from './components/product-edit/product-edit.component';
import { ProductRegisterComponent } from './components/product-register/product-register.component';
import { ProductsComponent } from './components/products/products.component';

const routes: Routes = [
  {
    path: '',
    component: DashboardLayoutComponent,
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
