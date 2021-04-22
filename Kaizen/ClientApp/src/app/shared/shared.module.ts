import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FlexLayoutModule } from '@angular/flex-layout';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { MaterialModule } from '@core/material.module';
import { AboutComponent } from '@shared/components/about/about.component';
import { DashboardCardComponent } from '@shared/components/dashboard-card/dashboard-card.component';
import { FooterComponent } from '@shared/components/footer/footer.component';
import { HeaderComponent } from '@shared/components/header/header.component';
import { HomeComponent } from '@shared/components/home/home.component';
import { NavMenuComponent } from '@shared/components/nav-menu/nav-menu.component';
import { OurservicesComponent } from '@shared/components/ourservices/ourservices.component';
import { Page404Component } from '@shared/components/page404/page404.component';
import { UniqueClientDirective } from '@shared/directives/unique-client.directive';
import { UniqueEmployeeDirective } from '@shared/directives/unique-employee.directive';
import { UniqueEquipmentDirective } from '@shared/directives/unique-equipment.directive';
import { UniqueProductDirective } from '@shared/directives/unique-product.directive';
import { UniqueUserDirective } from '@shared/directives/unique-user.directive';
import { ClientsFilterPipe } from '@shared/pipes/clients-filter.pipe';
import { MonthBitPipe } from '@shared/pipes/month-bit.pipe';
import { HttpErrorHandlerService } from '@shared/services/http-error-handler.service';
import { NotificationsService } from '@shared/services/notifications.service';
import { MatFileInputModule } from 'mat-file-input';
import { ConfirmDialogComponent } from './components/confirm-dialog/confirm-dialog.component';
import { DigitalSignatureComponent } from './components/digital-signature/digital-signature.component';
import { ErrorDialogComponent } from './components/error-dialog/error-dialog.component';
import { SelectDateModalComponent } from './components/select-date-modal/select-date-modal.component';
import { SuccessDialogComponent } from './components/success-dialog/success-dialog.component';
import { LoadingButtonDirective } from './directives/loading-button.directive';
import { DashboardLayoutComponent } from './layouts/dashboard-layout/dashboard-layout.component';
import { DefaultLayoutComponent } from './layouts/default-layout/default-layout.component';
import { ActivityStatePipe } from './pipes/activity-state.pipe';
import { ApplicationMonthsPipe } from './pipes/application-months.pipe';
import { ClientStatePipe } from './pipes/client-state.pipe';
import { FilterEmployeesPipe } from './pipes/filter-employees.pipe';
import { FilterEquipmentsPipe } from './pipes/filter-equipments.pipe';
import { FilterProductsPipe } from './pipes/filter-products.pipe';
import { InvoiceStatePipe } from './pipes/invoice-state.pipe';
import { ObservableWithStatusPipe } from './pipes/observable-with-status.pipe';
import { PaymentMethodPipe } from './pipes/payment-method.pipe';
import { PeriodicityPipe } from './pipes/periodicity.pipe';
import { ServiceRequestStatePipe } from './pipes/service-request-state.pipe';
import { WorkOrderStatePipe } from './pipes/work-order-state.pipe';
import { NotificationsComponent } from './components/notifications/notifications.component';
import { CardInfoItemComponent } from './components/card-info-item/card-info-item.component';

@NgModule({
  declarations: [
    AboutComponent,
    ConfirmDialogComponent,
    DashboardCardComponent,
    DashboardLayoutComponent,
    DefaultLayoutComponent,
    DigitalSignatureComponent,
    ErrorDialogComponent,
    FooterComponent,
    HeaderComponent,
    HomeComponent,
    NavMenuComponent,
    OurservicesComponent,
    Page404Component,
    SelectDateModalComponent,
    SuccessDialogComponent,

    LoadingButtonDirective,
    UniqueClientDirective,
    UniqueEmployeeDirective,
    UniqueEquipmentDirective,
    UniqueProductDirective,
    UniqueUserDirective,

    ActivityStatePipe,
    ApplicationMonthsPipe,
    CardInfoItemComponent,
    ClientsFilterPipe,
    ClientStatePipe,
    FilterEmployeesPipe,
    FilterEquipmentsPipe,
    FilterProductsPipe,
    InvoiceStatePipe,
    ObservableWithStatusPipe,
    MonthBitPipe,
    PaymentMethodPipe,
    PeriodicityPipe,
    ServiceRequestStatePipe,
    WorkOrderStatePipe,
    NotificationsComponent
  ],
  imports: [ CommonModule, FlexLayoutModule, FormsModule, MaterialModule, ReactiveFormsModule, RouterModule ],
  exports: [
    AboutComponent,
    CardInfoItemComponent,
    ConfirmDialogComponent,
    DashboardCardComponent,
    DigitalSignatureComponent,
    FooterComponent,
    HeaderComponent,
    HomeComponent,
    NavMenuComponent,
    OurservicesComponent,
    Page404Component,

    LoadingButtonDirective,
    UniqueClientDirective,
    UniqueEmployeeDirective,
    UniqueEquipmentDirective,
    UniqueProductDirective,
    UniqueUserDirective,

    CommonModule,
    MaterialModule,
    MatFileInputModule,
    FlexLayoutModule,

    ActivityStatePipe,
    ApplicationMonthsPipe,
    ClientsFilterPipe,
    ClientStatePipe,
    FilterEmployeesPipe,
    FilterEquipmentsPipe,
    FilterProductsPipe,
    InvoiceStatePipe,
    ObservableWithStatusPipe,
    MonthBitPipe,
    PaymentMethodPipe,
    PeriodicityPipe,
    ServiceRequestStatePipe,
    WorkOrderStatePipe
  ],
  providers: [ NotificationsService, HttpErrorHandlerService ]
})
export class SharedModule {}
