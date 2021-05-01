import { Component, Input, OnInit } from '@angular/core';
import { Invoice } from "@modules/payments/models/invoice";

@Component({
  selector: 'app-invoice-detail',
  templateUrl: './invoice-detail.component.html'
})
export class InvoiceDetailComponent implements OnInit {
  @Input('invoice') invoice: Invoice;

  constructor() {
  }

  ngOnInit(): void {
  }
}
