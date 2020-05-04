import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { EquipmentsComponent } from './components/equipments/equipments.component';
import { EquipmentRegisterComponent } from './components/equipment-register/equipment-register.component';
import { AuthGuard } from '@app/core/guards/auth.guard';
import { AdminGuard } from '@app/core/guards/admin.guard';

const routes: Routes = [
	{
		path: '',
		canActivate: [ AuthGuard, AdminGuard ],
		children: [
			{ path: '', component: EquipmentsComponent },
			{ path: 'register', component: EquipmentRegisterComponent }
		]
	}
];

@NgModule({
	imports: [ RouterModule.forChild(routes) ],
	exports: [ RouterModule ]
})
export class EquipmentsRoutingModule {}
