import { HttpClientTestingModule } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { CheckEquipmentExistsService } from './check-equipment-exists.service';

describe('CheckEquipmentExistsService', () => {
  let service: CheckEquipmentExistsService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ CheckEquipmentExistsService ],
      imports: [ HttpClientTestingModule ]
    });

    service = TestBed.inject(CheckEquipmentExistsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
