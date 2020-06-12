import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ActivityScheduleComponent } from './activity-schedule.component';

describe('ActivityScheduleComponent', () => {
  let component: ActivityScheduleComponent;
  let fixture: ComponentFixture<ActivityScheduleComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ActivityScheduleComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ActivityScheduleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
