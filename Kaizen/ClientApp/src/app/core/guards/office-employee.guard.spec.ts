import { TestBed } from '@angular/core/testing';

import { OfficeEmployeeGuard } from './office-employee.guard';

describe('OfficeEmployeeGuard', () => {
  let guard: OfficeEmployeeGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(OfficeEmployeeGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
