import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Page404Component } from '@shared/components/page404/page404.component';
import { HeaderComponent } from '@shared/components/header/header.component';
import { FooterComponent } from '@shared/components/footer/footer.component';
import { NavMenuComponent } from '@shared/components/nav-menu/nav-menu.component';
import { MaterialModule } from '@core/material.module';
import { RouterModule } from '@angular/router';
import { ContactComponent } from '@shared/components/contact/contact.component';
import { AboutComponent } from '@shared/components/about/about.component';
import { UniqueClientDirective } from '@shared/directives/unique-client.directive';
import { UniqueUserDirective } from '@shared/directives/unique-user.directive';
import { NotificationsService } from '@shared/services/notifications.service';
import { HttpErrorHandlerService } from '@shared/services/http-error-handler.service';
import { ClientsFilterPipe } from './pipes/clients-filter.pipe';
import { UniqueEmployeeDirective } from './directives/unique-employee.directive';
import { HomeComponent } from '@shared/components/home/home.component';
import { UniqueEquipmentDirective } from './directives/unique-equipment.directive';
import { UniqueProductDirective } from './directives/unique-product.directive';

@NgModule({
	declarations: [
		HomeComponent,
		Page404Component,
		HeaderComponent,
		FooterComponent,
		NavMenuComponent,
		ContactComponent,
		AboutComponent,
		UniqueClientDirective,
		UniqueUserDirective,
		ClientsFilterPipe,
		UniqueEmployeeDirective,
		UniqueEquipmentDirective,
		UniqueProductDirective
	],
	imports: [ CommonModule, MaterialModule, RouterModule ],
	exports: [
		MaterialModule,
		CommonModule,
		HomeComponent,
		Page404Component,
		HeaderComponent,
		FooterComponent,
		NavMenuComponent,
		ContactComponent,
		AboutComponent,
		UniqueClientDirective,
		UniqueUserDirective,
		UniqueEmployeeDirective,
		UniqueEquipmentDirective,
		UniqueProductDirective,
		ClientsFilterPipe
	],
	providers: [ NotificationsService, HttpErrorHandlerService ]
})
export class SharedModule {}
