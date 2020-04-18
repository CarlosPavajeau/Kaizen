import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ClientRoutingModule } from './client-routing.module';
import { ClientRegisterComponent } from './components/client-register/client-register.component';
import { ClientsComponent } from './components/clients/clients.component';


@NgModule({
  declarations: [
    ClientRegisterComponent,
    ClientsComponent
  ],
  imports: [
    CommonModule,
    ClientRoutingModule
  ]
})
export class ClientModule { }
