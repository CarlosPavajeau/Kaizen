import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { EquipmentsRoutingModule } from './equipments-routing.module';
import { EquipmentRegisterComponent } from './components/equipment-register/equipment-register.component';
import { EquipmentsComponent } from './components/equipments/equipments.component';
import { SharedModule } from '@app/shared/shared.module';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { EquipmentDetailComponent } from './components/equipment-detail/equipment-detail.component';

@NgModule({
  declarations: [ EquipmentRegisterComponent, EquipmentsComponent, EquipmentDetailComponent ],
  imports: [ CommonModule, EquipmentsRoutingModule, SharedModule, FormsModule, ReactiveFormsModule ]
})
export class EquipmentsModule {}
