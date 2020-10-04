import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { ActivityScheduleDayComponent } from './activity-schedule-day.component';

describe('ActivityScehduleDayComponent', () => {
  let component: ActivityScheduleDayComponent;
  let fixture: ComponentFixture<ActivityScheduleDayComponent>;

  beforeEach(
    async(() => {
      TestBed.configureTestingModule({
        declarations: [ ActivityScheduleDayComponent ]
      }).compileComponents();
    })
  );

  beforeEach(() => {
    fixture = TestBed.createComponent(ActivityScheduleDayComponent);
    component = fixture.componentInstance;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
