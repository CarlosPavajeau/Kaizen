import { HttpClientTestingModule } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';

import { EmployeeSignalrService } from './employee-signalr.service';

describe('EmployeeSignalrService', () => {
  let service: EmployeeSignalrService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [ HttpClientTestingModule ]
    });
    service = TestBed.inject(EmployeeSignalrService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
