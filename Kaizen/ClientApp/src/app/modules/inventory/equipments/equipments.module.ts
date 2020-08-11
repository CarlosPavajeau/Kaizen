import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { EquipmentsRoutingModule } from './equipments-routing.module';
import { EquipmentRegisterComponent } from './components/equipment-register/equipment-register.component';
import { EquipmentsComponent } from './components/equipments/equipments.component';
import { SharedModule } from '@app/shared/shared.module';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { EquipmentDetailComponent } from './components/equipment-detail/equipment-detail.component';
import { EquipmentEditComponent } from './components/equipment-edit/equipment-edit.component';

@NgModule({
  declarations: [ EquipmentRegisterComponent, EquipmentsComponent, EquipmentDetailComponent, EquipmentEditComponent ],
  imports: [ CommonModule, EquipmentsRoutingModule, SharedModule, FormsModule, ReactiveFormsModule ]
})
export class EquipmentsModule {}
