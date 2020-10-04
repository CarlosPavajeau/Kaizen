import { TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { SharedModule } from '../shared.module';
import { HttpErrorHandlerService } from './http-error-handler.service';

describe('HttpErrorHandlerService', () => {
  let service: HttpErrorHandlerService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ HttpErrorHandlerService ],
      imports: [ RouterTestingModule, SharedModule ]
    });

    service = TestBed.inject(HttpErrorHandlerService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
