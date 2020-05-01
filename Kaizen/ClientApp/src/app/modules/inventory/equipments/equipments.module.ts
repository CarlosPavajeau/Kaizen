import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { EquipmentsRoutingModule } from './equipments-routing.module';
import { EquipmentRegisterComponent } from './components/equipment-register/equipment-register.component';
import { EquipmentsComponent } from './components/equipments/equipments.component';

@NgModule({
	declarations: [EquipmentRegisterComponent, EquipmentsComponent],
	imports: [ CommonModule, EquipmentsRoutingModule ]
})
export class EquipmentsModule {}
