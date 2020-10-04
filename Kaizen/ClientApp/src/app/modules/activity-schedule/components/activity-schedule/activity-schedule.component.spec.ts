import { HttpClientTestingModule } from '@angular/common/http/testing';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { SharedModule } from '@app/shared/shared.module';
import { ActivityScheduleComponent } from './activity-schedule.component';

describe('ActivityScheduleComponent', () => {
  let component: ActivityScheduleComponent;
  let fixture: ComponentFixture<ActivityScheduleComponent>;

  beforeEach(
    async(() => {
      TestBed.configureTestingModule({
        imports: [ HttpClientTestingModule, SharedModule ],
        declarations: [ ActivityScheduleComponent ]
      }).compileComponents();
    })
  );

  beforeEach(() => {
    fixture = TestBed.createComponent(ActivityScheduleComponent);
    component = fixture.componentInstance;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
