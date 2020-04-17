import { NgModule } from '@angular/core';
import { Routes, RouterModule } from "@angular/router";
import { HomeComponent } from './home/home.component';
import { Page404Component } from './shared/components/page404/page404.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'user', loadChildren: () => import('./users/user.module').then(m => m.UserModule) },

  { path: '**', component: Page404Component }
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forRoot(routes, { enableTracing: false })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
