import { TestBed } from '@angular/core/testing';

import { CheckClientExistsService } from './check-client-exists.service';

describe('CheckClientExistsService', () => {
  let service: CheckClientExistsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CheckClientExistsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
