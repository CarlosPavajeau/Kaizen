import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { SharedModule } from '@app/shared/shared.module';
import { ActivityScheduleModule } from '../../activity-schedule.module';
import { ActivityButtonComponent } from './activity-button.component';

describe('ActivityButtonComponent', () => {
  let component: ActivityButtonComponent;
  let fixture: ComponentFixture<ActivityButtonComponent>;

  beforeEach(
    waitForAsync(() => {
      TestBed.configureTestingModule({
        imports: [ SharedModule, ActivityScheduleModule ],
        declarations: [ ActivityButtonComponent ]
      }).compileComponents();
    })
  );

  beforeEach(() => {
    fixture = TestBed.createComponent(ActivityButtonComponent);
    component = fixture.componentInstance;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
