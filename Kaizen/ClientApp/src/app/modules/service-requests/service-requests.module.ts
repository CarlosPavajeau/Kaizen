import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { ServiceRequestDetailComponent } from './components/service-request-detail/service-request-detail.component';
import { ServiceRequestRegisterComponent } from './components/service-request-register/service-request-register.component';
import { ServiceRequestsComponent } from './components/service-requests/service-requests.component';
import { ServiceRequestsRoutingModule } from './service-requests-routing.module';
import { SharedModule } from '@shared/shared.module';

@NgModule({
	declarations: [ ServiceRequestRegisterComponent, ServiceRequestDetailComponent, ServiceRequestsComponent ],
	imports: [ SharedModule, ServiceRequestsRoutingModule, FormsModule, ReactiveFormsModule ]
})
export class ServiceRequestsModule {}
