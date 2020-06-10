import { TestBed } from '@angular/core/testing';

import { BaseSignalrService } from './base-signalr.service';

describe('BaseSignalrService', () => {
  let service: BaseSignalrService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(BaseSignalrService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
