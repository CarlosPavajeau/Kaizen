import { TestBed } from '@angular/core/testing';

import { ClientRegisterGuard } from './client-register.guard';

describe('ClientRegisterGuard', () => {
  let guard: ClientRegisterGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(ClientRegisterGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
