import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { EquipmentsRoutingModule } from './equipments-routing.module';
import { EquipmentRegisterComponent } from './components/equipment-register/equipment-register.component';
import { EquipmentsComponent } from './components/equipments/equipments.component';
import { SharedModule } from '@app/shared/shared.module';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';

@NgModule({
	declarations: [ EquipmentRegisterComponent, EquipmentsComponent ],
	imports: [ CommonModule, EquipmentsRoutingModule, SharedModule, FormsModule, ReactiveFormsModule ]
})
export class EquipmentsModule {}
