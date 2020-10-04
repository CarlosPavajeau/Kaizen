import { HttpClientTestingModule } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { AuthenticationService } from '../authentication/authentication.service';
import { OfficeEmployeeGuard } from './office-employee.guard';

describe('OfficeEmployeeGuard', () => {
  let guard: OfficeEmployeeGuard;
  let service: AuthenticationService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ OfficeEmployeeGuard, AuthenticationService ],
      imports: [ HttpClientTestingModule, RouterTestingModule ]
    });

    guard = TestBed.inject(OfficeEmployeeGuard);
    service = TestBed.inject(AuthenticationService);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
