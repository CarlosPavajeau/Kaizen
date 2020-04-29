import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Page404Component } from '@shared/components/page404/page404.component';
import { HeaderComponent } from '@shared/components/header/header.component';
import { FooterComponent } from '@shared/components/footer/footer.component';
import { NavMenuComponent } from '@shared/nav-menu/nav-menu.component';
import { MaterialModule } from '@core/material.module';
import { RouterModule } from '@angular/router';
import { ContactComponent } from '@shared/components/contact/contact.component';
import { AboutComponent } from '@shared/components/about/about.component';
import { UniqueClientDirective } from '@shared/directives/unique-client.directive';
import { UniqueUserDirective } from '@shared/directives/unique-user.directive';
import { NotificationsService } from '@shared/services/notifications.service';
import { HttpErrorHandlerService } from '@shared/services/http-error-handler.service';
import { ClientsFilterPipe } from './pipes/clients-filter.pipe';

@NgModule({
  declarations: [
    Page404Component,
    HeaderComponent,
    FooterComponent,
    NavMenuComponent,
    ContactComponent,
    AboutComponent,
    UniqueClientDirective,
    UniqueUserDirective,
    ClientsFilterPipe
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
    AboutComponent,
    UniqueClientDirective,
    UniqueUserDirective
  ],
  providers: [
    NotificationsService,
    HttpErrorHandlerService
  ]
})
export class SharedModule { }
