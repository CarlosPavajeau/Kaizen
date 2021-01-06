import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AuthenticationService } from '@core/authentication/authentication.service';
import { CLIENT_ROLE } from '@global/roles';
import { InvoiceState } from '@modules/payments/models/invoice-state';
import { ProductInvoice } from '@modules/payments/models/product-invoice';
import { ProductInvoiceService } from '@modules/payments/services/product-invoice.service';
import { ObservableStatus } from '@shared/models/observable-with-status';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-product-invoice-detail',
  templateUrl: './product-invoice-detail.component.html',
  styleUrls: [ './product-invoice-detail.component.scss' ]
})
export class ProductInvoiceDetailComponent implements OnInit {
  public ObsStatus: typeof ObservableStatus = ObservableStatus;
  public InvoiceState: typeof InvoiceState = InvoiceState;

  productInvoice$: Observable<ProductInvoice>;
  isClient = false;

  constructor(
    private productInvoiceService: ProductInvoiceService,
    private activatedRoute: ActivatedRoute,
    private authService: AuthenticationService
  ) {}

  ngOnInit(): void {
    this.loadData();
  }

  private loadData(): void {
    const id = +this.activatedRoute.snapshot.paramMap.get('id');
    this.productInvoice$ = this.productInvoiceService.getProductInvoice(id);

    const role = this.authService.getUserRole();
    if (role === CLIENT_ROLE) {
      this.isClient = true;
    }
  }
}
