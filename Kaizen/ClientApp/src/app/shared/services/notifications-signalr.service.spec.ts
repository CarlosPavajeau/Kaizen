import { HttpClientTestingModule } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';

import { NotificationsSignalrService } from './notifications-signalr.service';

describe('NotificationsSignalrService', () => {
  let service: NotificationsSignalrService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [ HttpClientTestingModule ]
    });
    service = TestBed.inject(NotificationsSignalrService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
