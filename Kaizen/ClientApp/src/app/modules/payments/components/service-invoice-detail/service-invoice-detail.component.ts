import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ServiceInvoice } from '@modules/payments/models/service-invoice';
import { ServiceInvoiceService } from '@modules/payments/services/service-invoice.service';
import { ObservableStatus } from '@shared/models/observable-with-status';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-service-invoice-detail',
  templateUrl: './service-invoice-detail.component.html',
  styleUrls: [ './service-invoice-detail.component.scss' ]
})
export class ServiceInvoiceDetailComponent implements OnInit {
  public ObsStatus: typeof ObservableStatus = ObservableStatus;

  serviceInvoice$: Observable<ServiceInvoice>;

  constructor(private serviceInvoiceService: ServiceInvoiceService, private activatedRoute: ActivatedRoute) {}

  ngOnInit(): void {
    this.loadData();
  }

  private loadData(): void {
    const id = +this.activatedRoute.snapshot.paramMap.get('id');
    this.serviceInvoice$ = this.serviceInvoiceService.getServiceInvoice(id);
  }
}
