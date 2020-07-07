import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ClientScheduleComponent } from './client-schedule.component';

describe('ClientScheduleComponent', () => {
  let component: ClientScheduleComponent;
  let fixture: ComponentFixture<ClientScheduleComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ClientScheduleComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ClientScheduleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
