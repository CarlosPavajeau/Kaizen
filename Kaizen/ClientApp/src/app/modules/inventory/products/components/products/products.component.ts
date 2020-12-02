import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Product } from '@modules/inventory/products/models/product';
import { ProductService } from '@modules/inventory/products/services/product.service';
import { ObservableStatus } from '@shared/models/observable-with-status';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: [ './products.component.scss' ]
})
export class ProductsComponent implements OnInit, AfterViewInit {
  public ObsStatus: typeof ObservableStatus = ObservableStatus;

  products: Product[] = [];
  products$: Observable<Product[]>;

  dataSource: MatTableDataSource<Product> = new MatTableDataSource<Product>(this.products);
  displayedColumns: string[] = [ 'code', 'name', 'amount', 'presentation', 'price', 'months', 'options' ];

  @ViewChild(MatPaginator, { static: true })
  paginator: MatPaginator;

  @ViewChild(MatSort) sort: MatSort;

  constructor(private productService: ProductService) {}

  ngOnInit(): void {
    this.loadProducts();
  }

  private loadProducts(): void {
    this.products$ = this.productService.getProducts();
    this.products$.subscribe((products) => {
      this.products = products;
      this.dataSource.data = this.products;
    });
  }

  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }
}
