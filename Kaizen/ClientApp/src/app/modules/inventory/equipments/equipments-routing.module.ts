import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { EquipmentsComponent } from './components/equipments/equipments.component';
import { EquipmentRegisterComponent } from './components/equipment-register/equipment-register.component';
import { AuthGuard } from '@app/core/guards/auth.guard';
import { AdminGuard } from '@app/core/guards/admin.guard';
import { EquipmentDetailComponent } from './components/equipment-detail/equipment-detail.component';

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
