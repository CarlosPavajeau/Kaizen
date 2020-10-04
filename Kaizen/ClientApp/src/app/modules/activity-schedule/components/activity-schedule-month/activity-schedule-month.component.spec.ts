import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { SharedModule } from '@app/shared/shared.module';
import { ActivityScheduleMonthComponent } from './activity-schedule-month.component';

describe('ActivityScehduleMonthComponent', () => {
  let component: ActivityScheduleMonthComponent;
  let fixture: ComponentFixture<ActivityScheduleMonthComponent>;

  beforeEach(
    async(() => {
      TestBed.configureTestingModule({
        imports: [ SharedModule ],
        declarations: [ ActivityScheduleMonthComponent ]
      }).compileComponents();
    })
  );

  beforeEach(() => {
    fixture = TestBed.createComponent(ActivityScheduleMonthComponent);
    component = fixture.componentInstance;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
