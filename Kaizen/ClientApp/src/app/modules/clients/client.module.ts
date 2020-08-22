import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { UserModule } from '@modules/users/user.module';
import { SharedModule } from '@shared/shared.module';
import { ClientRoutingModule } from './client-routing.module';
import { ClientDetailComponent } from './components/client-detail/client-detail.component';
import { ClientRegisterComponent } from './components/client-register/client-register.component';
import { ClientRequestDetailComponent } from './components/client-request-detail/client-request-detail.component';
import { ClientRequestsComponent } from './components/client-requests/client-requests.component';
import { ClientsComponent } from './components/clients/clients.component';

@NgModule({
  declarations: [
    ClientRegisterComponent,
    ClientsComponent,
    ClientRequestsComponent,
    ClientRequestDetailComponent,
    ClientDetailComponent
  ],
  imports: [ FormsModule, ReactiveFormsModule, SharedModule, ClientRoutingModule, UserModule ]
})
export class ClientModule {}
