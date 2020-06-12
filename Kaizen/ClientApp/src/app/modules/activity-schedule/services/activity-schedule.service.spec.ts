import { TestBed } from '@angular/core/testing';

import { ActivityScheduleService } from './activity-schedule.service';

describe('ActivityScheduleService', () => {
  let service: ActivityScheduleService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ActivityScheduleService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
