import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { WorkOrderDetailComponent } from './work-order-detail.component';

describe('WorkOrderDetailComponent', () => {
  let component: WorkOrderDetailComponent;
  let fixture: ComponentFixture<WorkOrderDetailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ WorkOrderDetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WorkOrderDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
