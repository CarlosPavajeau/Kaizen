import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TodayStatisticsComponent } from './today-statistics.component';

describe('TodayStatisticsComponent', () => {
  let component: TodayStatisticsComponent;
  let fixture: ComponentFixture<TodayStatisticsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TodayStatisticsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TodayStatisticsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
