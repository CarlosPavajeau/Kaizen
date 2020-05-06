import { TestBed } from '@angular/core/testing';

import { CheckEntityExistsService } from './check-entity-exists.service';

describe('CheckEntityExistsService', () => {
  let service: CheckEntityExistsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CheckEntityExistsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
