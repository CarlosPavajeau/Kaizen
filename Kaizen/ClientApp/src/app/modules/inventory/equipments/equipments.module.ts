import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from '@shared/shared.module';
import { EquipmentDetailComponent } from './components/equipment-detail/equipment-detail.component';
import { EquipmentEditComponent } from './components/equipment-edit/equipment-edit.component';
import { EquipmentRegisterComponent } from './components/equipment-register/equipment-register.component';
import { EquipmentsComponent } from './components/equipments/equipments.component';
import { EquipmentsRoutingModule } from './equipments-routing.module';

@NgModule({
  declarations: [ EquipmentRegisterComponent, EquipmentsComponent, EquipmentDetailComponent, EquipmentEditComponent ],
  imports: [ CommonModule, EquipmentsRoutingModule, SharedModule, FormsModule, ReactiveFormsModule ]
})
export class EquipmentsModule {}
