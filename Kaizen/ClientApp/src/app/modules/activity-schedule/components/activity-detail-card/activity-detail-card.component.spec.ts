import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { ActivityDetailCardComponent } from './activity-detail-card.component';

describe('ActivityDetailCardComponent', () => {
  let component: ActivityDetailCardComponent;
  let fixture: ComponentFixture<ActivityDetailCardComponent>;

  beforeEach(
    waitForAsync(() => {
      TestBed.configureTestingModule({
        declarations: [ ActivityDetailCardComponent ]
      }).compileComponents();
    })
  );

  beforeEach(() => {
    fixture = TestBed.createComponent(ActivityDetailCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
