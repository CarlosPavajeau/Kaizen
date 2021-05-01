import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AuthenticationService } from '@core/authentication/authentication.service';
import { CLIENT_ROLE } from '@global/roles';
import { ProductInvoice } from '@modules/payments/models/product-invoice';
import { ProductInvoiceService } from '@modules/payments/services/product-invoice.service';
import { ObservableStatus } from '@shared/models/observable-with-status';
import { Person } from '@shared/models/person';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-product-invoices',
  templateUrl: './product-invoices.component.html'
})
export class ProductInvoicesComponent implements OnInit {
  public ObsStatus: typeof ObservableStatus = ObservableStatus;

  productInvoices: ProductInvoice[];
  productInvoices$: Observable<ProductInvoice[]>;

  dataSource: MatTableDataSource<ProductInvoice> = new MatTableDataSource<ProductInvoice>([]);
  displayedColumns: string[] = [ 'id', 'state', 'iva', 'subtotal', 'total', 'actions' ];

  @ViewChild(MatPaginator, { static: true })
  paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  isClient = false;

  constructor(private productInvoiceService: ProductInvoiceService, private authService: AuthenticationService) {}

  ngOnInit(): void {
    this.loadData();
  }

  private loadData(): void {
    const role = this.authService.getUserRole();
    if (role === CLIENT_ROLE) {
      this.isClient = true;
      const currentPerson: Person = JSON.parse(localStorage.getItem('current_person'));
      this.productInvoices$ = this.productInvoiceService.getClientProductInvoices(currentPerson.id);
    } else {
      this.productInvoices$ = this.productInvoiceService.getProductInvoices();
    }

    this.productInvoices$.subscribe((productInvoices) => {
      this.dataSource.data = productInvoices;
    });
  }
}
