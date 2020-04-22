import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ClientRoutingModule } from './client-routing.module';
import { ClientRegisterComponent } from './components/client-register/client-register.component';
import { ClientsComponent } from './components/clients/clients.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    ClientRegisterComponent,
    ClientsComponent
  ],
  imports: [
    FormsModule,
    ReactiveFormsModule,
    SharedModule,
    ClientRoutingModule
  ]
})
export class ClientModule { }
