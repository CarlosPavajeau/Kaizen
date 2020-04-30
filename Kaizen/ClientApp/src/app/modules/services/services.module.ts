import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ServicesRoutingModule } from './services-routing.module';
import { ServiceRegisterComponent } from './components/service-register/service-register.component';
import { ServicesComponent } from './components/services/services.component';
import { ServiceRequestComponent } from './components/service-request/service-request.component';

@NgModule({
	declarations: [ ServiceRegisterComponent, ServicesComponent, ServiceRequestComponent ],
	imports: [ CommonModule, ServicesRoutingModule ]
})
export class ServicesModule {}
