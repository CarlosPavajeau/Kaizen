import { TestBed } from '@angular/core/testing';

import { AdminOrOfficeEmployeeGuard } from './admin-or-office-employee.guard';

describe('AdminOrOfficeEmployeeGuard', () => {
  let guard: AdminOrOfficeEmployeeGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(AdminOrOfficeEmployeeGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
