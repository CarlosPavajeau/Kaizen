import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminGuard } from '@app/core/guards/admin.guard';
import { AuthGuard } from '@app/core/guards/auth.guard';
import { EquipmentDetailComponent } from './components/equipment-detail/equipment-detail.component';
import { EquipmentEditComponent } from './components/equipment-edit/equipment-edit.component';
import { EquipmentRegisterComponent } from './components/equipment-register/equipment-register.component';
import { EquipmentsComponent } from './components/equipments/equipments.component';

const routes: Routes = [
  {
    path: '',
    canActivate: [ AuthGuard, AdminGuard ],
    children: [
      {
        path: '',
        component: EquipmentsComponent
      },
      {
        path: 'register',
        component: EquipmentRegisterComponent
      },
      {
        path: 'edit/:code',
        component: EquipmentEditComponent
      },
      {
        path: ':code',
        component: EquipmentDetailComponent
      }
    ]
  }
];

@NgModule({
  imports: [ RouterModule.forChild(routes) ],
  exports: [ RouterModule ]
})
export class EquipmentsRoutingModule {}
