import { HttpClientTestingModule } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { NewServiceRequestSignalrService } from './new-service-request-signalr.service';

describe('ServiceRequestSignalrService', () => {
  let service: NewServiceRequestSignalrService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [ HttpClientTestingModule ]
    });
    service = TestBed.inject(NewServiceRequestSignalrService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
