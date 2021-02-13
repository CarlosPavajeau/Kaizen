import { HttpClientTestingModule } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { ServiceRequestSignalrService } from './service-request-signalr.service';

describe('ServiceRequestSignalrService', () => {
  let service: ServiceRequestSignalrService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [ HttpClientTestingModule ]
    });
    service = TestBed.inject(ServiceRequestSignalrService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
