import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { Product } from '../../models/product';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { ProductService } from '../../services/product.service';

@Component({
	selector: 'app-products',
	templateUrl: './products.component.html',
	styleUrls: [ './products.component.css' ]
})
export class ProductsComponent implements OnInit, AfterViewInit {
	products: Product[];
	dataSource: MatTableDataSource<Product> = new MatTableDataSource<Product>(this.products);
	displayedColumns: string[] = [ 'code', 'name', 'amount', 'presentation', 'price' ];
	@ViewChild(MatPaginator, { static: true })
	paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;

	constructor(private productService: ProductService) {}

	ngOnInit(): void {
		this.loadProducts();
	}

	private loadProducts(): void {
		this.productService.getProducts().subscribe((products) => {
			this.products = products;
			this.dataSource.data = this.products;
		});
	}

	ngAfterViewInit(): void {
		this.dataSource.paginator = this.paginator;
		this.dataSource.sort = this.sort;
	}
}
