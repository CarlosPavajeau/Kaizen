import { AboutComponent } from '@shared/components/about/about.component';
import { ClientsFilterPipe } from '@shared/pipes/clients-filter.pipe';
import { CommonModule } from '@angular/common';
import { DashboardCardComponent } from '@shared/components/dashboard-card/dashboard-card.component';
import { FooterComponent } from '@shared/components/footer/footer.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HeaderComponent } from '@shared/components/header/header.component';
import { HomeComponent } from '@shared/components/home/home.component';
import { HttpErrorHandlerService } from '@shared/services/http-error-handler.service';
import { MatAutocompleteChipListInputComponent } from './components/mat-autocomplete-chip-list-input/mat-autocomplete-chip-list-input.component';
import { MaterialFileInputModule } from 'ngx-material-file-input';
import { MaterialModule } from '@core/material.module';
import { MonthBitPipe } from '@shared/pipes/month-bit.pipe';
import { NavMenuComponent } from '@shared/components/nav-menu/nav-menu.component';
import { NgModule } from '@angular/core';
import { NotificationsService } from '@shared/services/notifications.service';
import { OurservicesComponent } from '@shared/components/ourservices/ourservices.component';
import { Page404Component } from '@shared/components/page404/page404.component';
import { RouterModule } from '@angular/router';
import { UniqueClientDirective } from '@shared/directives/unique-client.directive';
import { UniqueEmployeeDirective } from '@shared/directives/unique-employee.directive';
import { UniqueEquipmentDirective } from '@shared/directives/unique-equipment.directive';
import { UniqueProductDirective } from '@shared/directives/unique-product.directive';
import { UniqueUserDirective } from '@shared/directives/unique-user.directive';

@NgModule({
	declarations: [
		HomeComponent,
		Page404Component,
		HeaderComponent,
		FooterComponent,
		NavMenuComponent,
		AboutComponent,
		UniqueClientDirective,
		UniqueUserDirective,
		ClientsFilterPipe,
		UniqueEmployeeDirective,
		UniqueEquipmentDirective,
		UniqueProductDirective,
		OurservicesComponent,
		DashboardCardComponent,
		MonthBitPipe,
		MatAutocompleteChipListInputComponent
	],
	imports: [ CommonModule, MaterialModule, RouterModule, FormsModule, ReactiveFormsModule ],
	exports: [
		MaterialModule,
		MaterialFileInputModule,
		CommonModule,
		HomeComponent,
		Page404Component,
		HeaderComponent,
		FooterComponent,
		NavMenuComponent,
		AboutComponent,
		UniqueClientDirective,
		UniqueUserDirective,
		UniqueEmployeeDirective,
		UniqueEquipmentDirective,
		UniqueProductDirective,
		ClientsFilterPipe,
		MonthBitPipe,
		OurservicesComponent,
		DashboardCardComponent,
		MatAutocompleteChipListInputComponent
	],
	providers: [ NotificationsService, HttpErrorHandlerService ]
})
export class SharedModule {}
