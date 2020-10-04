import { HttpClientTestingModule } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { CheckClientExistsService } from './check-client-exists.service';

describe('CheckClientExistsService', () => {
  let service: CheckClientExistsService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ CheckClientExistsService ],
      imports: [ HttpClientTestingModule ]
    });

    service = TestBed.inject(CheckClientExistsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
