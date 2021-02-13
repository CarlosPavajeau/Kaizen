import { HttpClientTestingModule } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { ActivitySignalrService } from './activity-signalr.service';

describe('NewActivitySignalrService', () => {
  let service: ActivitySignalrService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [ HttpClientTestingModule ]
    });
    service = TestBed.inject(ActivitySignalrService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
