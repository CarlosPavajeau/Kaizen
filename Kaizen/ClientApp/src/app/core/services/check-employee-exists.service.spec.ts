import { HttpClientTestingModule } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { CheckEmployeeExistsService } from './check-employee-exists.service';

describe('CheckEmployeeExistsService', () => {
  let service: CheckEmployeeExistsService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ CheckEmployeeExistsService ],
      imports: [ HttpClientTestingModule ]
    });

    service = TestBed.inject(CheckEmployeeExistsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
