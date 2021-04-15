import { NgModule } from '@angular/core';
import { SharedModule } from '@shared/shared.module';

import { CertificatesRoutingModule } from './certificates-routing.module';
import { CertificateDetailComponent } from './components/certificate-detail/certificate-detail.component';
import { CertificatesComponent } from './components/certificates/certificates.component';


@NgModule({
  declarations: [
    CertificateDetailComponent,
    CertificatesComponent
  ],
  imports: [
    SharedModule,
    CertificatesRoutingModule
  ]
})
export class CertificatesModule {
}
