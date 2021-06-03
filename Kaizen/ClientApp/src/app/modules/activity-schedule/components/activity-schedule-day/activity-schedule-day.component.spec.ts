import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { ActivityScheduleDayComponent } from './activity-schedule-day.component';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { SharedModule } from '@shared/shared.module';

describe('ActivityScehduleDayComponent', () => {
  let component: ActivityScheduleDayComponent;
  let fixture: ComponentFixture<ActivityScheduleDayComponent>;

  beforeEach(
    waitForAsync(() => {
      TestBed.configureTestingModule({
        imports: [ HttpClientTestingModule, SharedModule ],
        declarations: [ ActivityScheduleDayComponent ],
      }).compileComponents();
    }),
  );

  beforeEach(() => {
    fixture = TestBed.createComponent(ActivityScheduleDayComponent);
    component = fixture.componentInstance;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
