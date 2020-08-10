import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../services/product.service';
import { ActivatedRoute } from '@angular/router';
import { Product } from '../../models/product';

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: [ './product-detail.component.css' ]
})
export class ProductDetailComponent implements OnInit {
  product: Product;

  constructor(private productService: ProductService, private activatedRoute: ActivatedRoute) {}

  ngOnInit(): void {
    this.loadData();
  }

  private loadData(): void {
    const code = this.activatedRoute.snapshot.paramMap.get('code');
    this.productService.getProduct(code).subscribe((product) => {
      this.product = product;
    });
  }
}
