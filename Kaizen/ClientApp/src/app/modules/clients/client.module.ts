import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { ClientRoutingModule } from './client-routing.module';
import { ClientRegisterComponent } from './components/client-register/client-register.component';
import { ClientsComponent } from './components/clients/clients.component';
import { SharedModule } from '@shared/shared.module';
import { UserModule } from '@modules/users/user.module';

@NgModule({
	declarations: [ ClientRegisterComponent, ClientsComponent ],
	imports: [ FormsModule, ReactiveFormsModule, SharedModule, ClientRoutingModule, UserModule ]
})
export class ClientModule {}
