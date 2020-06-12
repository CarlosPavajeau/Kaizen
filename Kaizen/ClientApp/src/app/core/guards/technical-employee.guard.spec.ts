import { TestBed } from '@angular/core/testing';

import { TechnicalEmployeeGuard } from './technical-employee.guard';

describe('TechnicalEmployeeGuard', () => {
  let guard: TechnicalEmployeeGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(TechnicalEmployeeGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
