import { HttpClientTestingModule } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { CheckEntityExistsService } from './check-entity-exists.service';

describe('CheckEntityExistsService', () => {
  let service: CheckEntityExistsService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ CheckEntityExistsService ],
      imports: [ HttpClientTestingModule ]
    });
    service = TestBed.inject(CheckEntityExistsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
