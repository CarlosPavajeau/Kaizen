import { TestBed } from '@angular/core/testing';

import { NewActivitySignalrService } from './new-activity-signalr.service';

describe('NewActivitySignalrService', () => {
  let service: NewActivitySignalrService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(NewActivitySignalrService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
