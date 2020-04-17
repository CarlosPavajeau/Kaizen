import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Page404Component } from './components/page404/page404.component';
import { HeaderComponent } from './components/header/header.component';
import { FooterComponent } from './components/footer/footer.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { MaterialModule } from '../core/material.module';

@NgModule({
  declarations: [
    Page404Component,
    HeaderComponent,
    FooterComponent,
    NavMenuComponent
  ],
  imports: [
    CommonModule,
    MaterialModule,
  ],
  exports: [
    Page404Component,
    HeaderComponent,
    FooterComponent,
    NavMenuComponent,
    MaterialModule
  ]
})
export class SharedModule { }
