import { HttpClientTestingModule } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { ProductInvoiceService } from './product-invoice.service';

describe('ProductInvoiceService', () => {
  let service: ProductInvoiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [ HttpClientTestingModule ]
    });
    service = TestBed.inject(ProductInvoiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
