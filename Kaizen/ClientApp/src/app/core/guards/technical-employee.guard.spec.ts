import { HttpClientTestingModule } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { AuthenticationService } from '@core/authentication/authentication.service';
import { TechnicalEmployeeGuard } from './technical-employee.guard';

describe('TechnicalEmployeeGuard', () => {
  let guard: TechnicalEmployeeGuard;
  let service: AuthenticationService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ TechnicalEmployeeGuard, AuthenticationService ],
      imports: [ HttpClientTestingModule, RouterTestingModule ]
    });

    guard = TestBed.inject(TechnicalEmployeeGuard);
    service = TestBed.inject(AuthenticationService);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
