import { TestBed } from '@angular/core/testing';
import { AppModule } from '@app/app.module';

import { DialogsService } from './dialogs.service';

describe('DialogsService', () => {
  let service: DialogsService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [ AppModule ]
    });
    service = TestBed.inject(DialogsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
