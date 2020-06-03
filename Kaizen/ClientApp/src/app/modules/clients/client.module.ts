import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { ClientRoutingModule } from './client-routing.module';
import { ClientRegisterComponent } from './components/client-register/client-register.component';
import { ClientsComponent } from './components/clients/clients.component';
import { SharedModule } from '@shared/shared.module';
import { UserModule } from '@modules/users/user.module';
import { ClientRequestsComponent } from './components/client-requests/client-requests.component';
import { ClientRequestDetailComponent } from './components/client-request-detail/client-request-detail.component';

@NgModule({
	declarations: [ ClientRegisterComponent, ClientsComponent, ClientRequestsComponent, ClientRequestDetailComponent ],
	imports: [ FormsModule, ReactiveFormsModule, SharedModule, ClientRoutingModule, UserModule ]
})
export class ClientModule {}
