import { TestBed } from '@angular/core/testing';
import { AppModule } from '@app/app.module';
import { NotificationsService } from './notifications.service';

describe('NotificationsService', () => {
  let service: NotificationsService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ NotificationsService ],
      imports: [ AppModule ]
    });

    service = TestBed.inject(NotificationsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
