import { TestBed } from '@angular/core/testing';

import { ServiceInvoiceService } from './service-invoice.service';

describe('ServiceInvoiceService', () => {
  let service: ServiceInvoiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ServiceInvoiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
