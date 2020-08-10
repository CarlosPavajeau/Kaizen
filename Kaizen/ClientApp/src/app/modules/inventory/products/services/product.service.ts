import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Product } from '@modules/inventory/products/models/product';
import { PRODUCTS_API_URL } from '@global/endpoints';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  constructor(private http: HttpClient) {}

  getProducts(): Observable<Product[]> {
    return this.http.get<Product[]>(PRODUCTS_API_URL);
  }

  getProduct(code: string): Observable<Product> {
    return this.http.get<Product>(`${PRODUCTS_API_URL}/${code}`);
  }

  saveProduct(product: Product): Observable<Product> {
    return this.http.post<Product>(PRODUCTS_API_URL, product);
  }

  updateProduct(product: Product): Observable<Product> {
    return this.http.put<Product>(`${PRODUCTS_API_URL}/${product.code}`, product);
  }
}
