import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ServiceRequestNewDateComponent } from './service-request-new-date.component';

describe('ServiceRequestNewDateComponent', () => {
  let component: ServiceRequestNewDateComponent;
  let fixture: ComponentFixture<ServiceRequestNewDateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ServiceRequestNewDateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ServiceRequestNewDateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
