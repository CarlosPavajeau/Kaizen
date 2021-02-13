import { HttpClientTestingModule } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';

import { EmployeeLocationService } from './employee-location.service';

describe('EmployeeLocationService', () => {
  let service: EmployeeLocationService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [ HttpClientTestingModule ]
    });
    service = TestBed.inject(EmployeeLocationService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
