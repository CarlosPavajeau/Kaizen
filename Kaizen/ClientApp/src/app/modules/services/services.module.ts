import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ServiceRegisterComponent } from '@modules/services/components/service-register/service-register.component';
import { ServicesComponent } from '@modules/services/components/services/services.component';
import { ServicesRoutingModule } from '@modules/services/services-routing.module';
import { SharedModule } from '@shared/shared.module';
import { SelectEmployeesComponent } from './components/select-employees/select-employees.component';
import { SelectEquipmentsComponent } from './components/select-equipments/select-equipments.component';
import { SelectProductsComponent } from './components/select-products/select-products.component';
import { ServiceDetailComponent } from './components/service-detail/service-detail.component';
import { ServiceEditComponent } from './components/service-edit/service-edit.component';

@NgModule({
  declarations: [
    ServiceRegisterComponent,
    ServicesComponent,
    ServiceDetailComponent,
    ServiceEditComponent,
    SelectEmployeesComponent,
    SelectEquipmentsComponent,
    SelectProductsComponent
  ],
  imports: [ FormsModule, ReactiveFormsModule, SharedModule, ServicesRoutingModule ]
})
export class ServicesModule {}
