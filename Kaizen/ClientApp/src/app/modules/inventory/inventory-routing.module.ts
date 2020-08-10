import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  { path: '', redirectTo: '/', pathMatch: 'full' },
  { path: 'products', loadChildren: () => import('./products/products.module').then((m) => m.ProductsModule) },
  { path: 'equipments', loadChildren: () => import('./equipments/equipments.module').then((m) => m.EquipmentsModule) }
];

@NgModule({
  imports: [ RouterModule.forChild(routes) ],
  exports: [ RouterModule ]
})
export class InventoryRoutingModule {}
