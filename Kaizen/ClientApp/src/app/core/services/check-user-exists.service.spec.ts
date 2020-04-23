import { TestBed } from '@angular/core/testing';

import { CheckUserExistsService } from './check-user-exists.service';

describe('CheckUserExistsService', () => {
  let service: CheckUserExistsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CheckUserExistsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
