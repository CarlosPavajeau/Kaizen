import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ServiceInvoice } from '@modules/payments/models/service-invoice';
import { ServiceInvoiceService } from '@modules/payments/services/service-invoice.service';

@Component({
  selector: 'app-service-invoice-detail',
  templateUrl: './service-invoice-detail.component.html',
  styleUrls: [ './service-invoice-detail.component.css' ]
})
export class ServiceInvoiceDetailComponent implements OnInit {
  serviceInvoice: ServiceInvoice;

  constructor(private serviceInvoiceService: ServiceInvoiceService, private activatedRoute: ActivatedRoute) {}

  ngOnInit(): void {
    this.loadData();
  }

  private loadData(): void {
    const id = +this.activatedRoute.snapshot.paramMap.get('id');
    this.serviceInvoiceService.getServiceInvoice(id).subscribe((serviceInvoice: ServiceInvoice) => {
      this.serviceInvoice = serviceInvoice;
      console.log(serviceInvoice);
    });
  }
}
