import { NgModule } from '@angular/core';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';

import { ServicesRoutingModule } from '@modules/services/services-routing.module';
import { ServiceRegisterComponent } from '@modules/services/components/service-register/service-register.component';
import { ServicesComponent } from '@modules/services/components/services/services.component';
import { SharedModule } from '@shared/shared.module';

@NgModule({
	declarations: [ ServiceRegisterComponent, ServicesComponent ],
	imports: [ FormsModule, ReactiveFormsModule, SharedModule, ServicesRoutingModule ]
})
export class ServicesModule {}
