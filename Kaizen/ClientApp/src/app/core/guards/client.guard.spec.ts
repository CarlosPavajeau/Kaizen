import { HttpClientTestingModule } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { AuthenticationService } from '@core/authentication/authentication.service';
import { ClientGuard } from './client.guard';

describe('ClientGuard', () => {
  let guard: ClientGuard;
  let service: AuthenticationService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ ClientGuard, AuthenticationService ],
      imports: [ HttpClientTestingModule, RouterTestingModule ]
    });

    guard = TestBed.inject(ClientGuard);
    service = TestBed.inject(AuthenticationService);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
