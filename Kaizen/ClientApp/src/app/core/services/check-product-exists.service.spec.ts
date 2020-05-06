import { TestBed } from '@angular/core/testing';

import { CheckProductExistsService } from './check-product-exists.service';

describe('CheckProductExistsService', () => {
  let service: CheckProductExistsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CheckProductExistsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
