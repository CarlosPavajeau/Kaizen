import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Page404Component } from './components/page404/page404.component';
import { HeaderComponent } from './components/header/header.component';
import { FooterComponent } from './components/footer/footer.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { MaterialModule } from '../core/material.module';
import { RouterModule } from '@angular/router';
import { ContactComponent } from './components/contact/contact.component';
import { AboutComponent } from './components/about/about.component';

@NgModule({
  declarations: [
    Page404Component,
    HeaderComponent,
    FooterComponent,
    NavMenuComponent,
    ContactComponent,
    AboutComponent
  ],
  imports: [
    CommonModule,
    MaterialModule,
    RouterModule
  ],
  exports: [
    MaterialModule,
    CommonModule,
    Page404Component,
    HeaderComponent,
    FooterComponent,
    NavMenuComponent,
    ContactComponent,
    AboutComponent
  ]
})
export class SharedModule { }
