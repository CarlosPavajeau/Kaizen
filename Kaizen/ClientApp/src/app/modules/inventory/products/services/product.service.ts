import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Product } from '@modules/inventory/products/models/product';
import { Endpoints } from '@global/endpoints';

@Injectable({
	providedIn: 'root'
})
export class ProductService {
	constructor(private http: HttpClient) {}

	getProducts(): Observable<Product[]> {
		return this.http.get<Product[]>(Endpoints.ProductsUrl);
	}

	getProduct(code: string): Observable<Product> {
		return this.http.get<Product>(`${Endpoints.ProductsUrl}/${code}`);
	}

	saveProduct(product: Product): Observable<Product> {
		return this.http.post<Product>(Endpoints.ProductsUrl, product);
	}

	updateProduct(product: Product): Observable<Product> {
		return this.http.put<Product>(`${Endpoints.ProductsUrl}/${product.code}`, product);
	}
}
