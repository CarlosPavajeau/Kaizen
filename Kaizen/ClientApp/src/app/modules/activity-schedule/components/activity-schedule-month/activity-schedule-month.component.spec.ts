import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { SharedModule } from '@app/shared/shared.module';
import { ActivityScheduleMonthComponent } from './activity-schedule-month.component';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('ActivityScehduleMonthComponent', () => {
  let component: ActivityScheduleMonthComponent;
  let fixture: ComponentFixture<ActivityScheduleMonthComponent>;

  beforeEach(
    waitForAsync(() => {
      TestBed.configureTestingModule({
        imports: [ HttpClientTestingModule, SharedModule ],
        declarations: [ ActivityScheduleMonthComponent ],
      }).compileComponents();
    }),
  );

  beforeEach(() => {
    fixture = TestBed.createComponent(ActivityScheduleMonthComponent);
    component = fixture.componentInstance;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
