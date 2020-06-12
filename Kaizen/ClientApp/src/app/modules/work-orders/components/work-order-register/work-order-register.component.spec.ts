import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { WorkOrderRegisterComponent } from './work-order-register.component';

describe('WorkOrderRegisterComponent', () => {
  let component: WorkOrderRegisterComponent;
  let fixture: ComponentFixture<WorkOrderRegisterComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ WorkOrderRegisterComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WorkOrderRegisterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
