import { AboutComponent } from '@shared/components/about/about.component';
import { ClientsFilterPipe } from '@shared/pipes/clients-filter.pipe';
import { CommonModule } from '@angular/common';
import { DashboardCardComponent } from '@shared/components/dashboard-card/dashboard-card.component';
import { DigitalSignatureComponent } from './components/digital-signature/digital-signature.component';
import { FlexLayoutModule } from '@angular/flex-layout';
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
import { PeriodicityPipe } from './pipes/periodicity.pipe';
import { ServiceRequestStatePipe } from './pipes/service-request-state.pipe';
import { RouterModule } from '@angular/router';
import { SelectDateModalComponent } from './components/select-date-modal/select-date-modal.component';
import { UniqueClientDirective } from '@shared/directives/unique-client.directive';
import { UniqueEmployeeDirective } from '@shared/directives/unique-employee.directive';
import { UniqueEquipmentDirective } from '@shared/directives/unique-equipment.directive';
import { UniqueProductDirective } from '@shared/directives/unique-product.directive';
import { UniqueUserDirective } from '@shared/directives/unique-user.directive';
import { ActivityStatePipe } from './pipes/activity-state.pipe';
import { ConfirmDialogComponent } from './components/confirm-dialog/confirm-dialog.component';
import { ClientStatePipe } from './pipes/client-state.pipe';
import { FilterProductsPipe } from './pipes/filter-products.pipe';
import { FilterEquipmentsPipe } from './pipes/filter-equipments.pipe';
import { FilterEmployeesPipe } from './pipes/filter-employees.pipe';
import { InvoiceStatePipe } from './pipes/invoice-state.pipe';
import { PaymentMethodPipe } from './pipes/payment-method.pipe';
import { SuccessDialogComponent } from './components/success-dialog/success-dialog.component';
import { ErrorDialogComponent } from './components/error-dialog/error-dialog.component';

@NgModule({
  declarations: [
    HomeComponent,
    Page404Component,
    HeaderComponent,
    FooterComponent,
    NavMenuComponent,
    AboutComponent,
    OurservicesComponent,
    DashboardCardComponent,
    MatAutocompleteChipListInputComponent,
    UniqueClientDirective,
    UniqueEmployeeDirective,
    UniqueEquipmentDirective,
    UniqueProductDirective,
    UniqueUserDirective,
    ClientsFilterPipe,
    MonthBitPipe,
    SelectDateModalComponent,
    PeriodicityPipe,
    ServiceRequestStatePipe,
    DigitalSignatureComponent,
    ActivityStatePipe,
    ConfirmDialogComponent,
    ClientStatePipe,
    FilterProductsPipe,
    FilterEquipmentsPipe,
    FilterEmployeesPipe,
    InvoiceStatePipe,
    PaymentMethodPipe,
    SuccessDialogComponent,
    ErrorDialogComponent
  ],
  imports: [ CommonModule, FlexLayoutModule, MaterialModule, RouterModule, FormsModule, ReactiveFormsModule ],
  exports: [
    CommonModule,
    MaterialModule,
    MaterialFileInputModule,
    FlexLayoutModule,
    HomeComponent,
    Page404Component,
    HeaderComponent,
    FooterComponent,
    NavMenuComponent,
    AboutComponent,
    OurservicesComponent,
    DashboardCardComponent,
    MatAutocompleteChipListInputComponent,
    DigitalSignatureComponent,
    UniqueClientDirective,
    UniqueUserDirective,
    UniqueEmployeeDirective,
    UniqueEquipmentDirective,
    UniqueProductDirective,
    ClientsFilterPipe,
    MonthBitPipe,
    PeriodicityPipe,
    ServiceRequestStatePipe,
    ActivityStatePipe,
    ConfirmDialogComponent,
    ClientStatePipe,
    FilterProductsPipe,
    FilterEquipmentsPipe,
    FilterEmployeesPipe,
    InvoiceStatePipe,
    PaymentMethodPipe
  ],
  providers: [ NotificationsService, HttpErrorHandlerService ]
})
export class SharedModule {}
