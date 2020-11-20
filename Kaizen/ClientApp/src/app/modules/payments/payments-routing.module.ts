import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from '@core/guards/auth.guard';
import { ClientGuard } from '@core/guards/client.guard';
import { DashboardLayoutComponent } from '@shared/layouts/dashboard-layout/dashboard-layout.component';
import { PayServiceInvoiceComponent } from './components/pay-service-invoice/pay-service-invoice.component';
import { ServiceInvoiceDetailComponent } from './components/service-invoice-detail/service-invoice-detail.component';
import { ServiceInvoicesComponent } from './components/service-invoices/service-invoices.component';

const routes: Routes = [
  {
    path: '',
    canActivate: [ AuthGuard ],
    children: [
      {
        component: DashboardLayoutComponent,
        path: 'service_invoices',
        children: [
          {
            path: '',
            component: ServiceInvoicesComponent
          },
          {
            path: ':id',
            component: ServiceInvoiceDetailComponent
          }
        ]
      },
      {
        path: 'pay/service_invoice/:id',
        component: PayServiceInvoiceComponent,
        canActivate: [ ClientGuard ]
      }
    ]
  }
];

@NgModule({
  imports: [ RouterModule.forChild(routes) ],
  exports: [ RouterModule ]
})
export class PaymentsRoutingModule {}
