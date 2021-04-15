import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardLayoutComponent } from '@app/shared/layouts/dashboard-layout/dashboard-layout.component';
import { CertificateDetailComponent } from './components/certificate-detail/certificate-detail.component';
import { CertificatesComponent } from './components/certificates/certificates.component';

const routes: Routes = [
  {
    path: '',
    component: DashboardLayoutComponent,
    children: [
      {
        path: '',
        component: CertificatesComponent
      },
      {
        path: ':id',
        component: CertificateDetailComponent
      }
    ]
  }
];

@NgModule({
  imports: [ RouterModule.forChild(routes) ],
  exports: [ RouterModule ]
})
export class CertificatesRoutingModule {
}
