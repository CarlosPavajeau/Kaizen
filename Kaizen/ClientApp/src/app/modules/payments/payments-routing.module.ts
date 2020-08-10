import { AuthGuard } from '@core/guards/auth.guard';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ServiceInvoicesComponent } from './components/service-invoices/service-invoices.component';

const routes: Routes = [
  {
    path: '',
    canActivate: [ AuthGuard ],
    children: [
      {
        path: 'service_invoices',
        component: ServiceInvoicesComponent
      }
    ]
  }
];

@NgModule({
  imports: [ RouterModule.forChild(routes) ],
  exports: [ RouterModule ]
})
export class PaymentsRoutingModule {}
