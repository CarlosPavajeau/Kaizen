import { HttpClientTestingModule } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { ClientSignalrService } from './client-signalr.service';

describe('NewClientSignalrService', () => {
  let service: ClientSignalrService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [ HttpClientTestingModule ]
    });
    service = TestBed.inject(ClientSignalrService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
