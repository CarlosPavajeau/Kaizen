import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ServiceRequestRegisterComponent } from './service-request-register.component';

describe('ServiceRequestRegisterComponent', () => {
  let component: ServiceRequestRegisterComponent;
  let fixture: ComponentFixture<ServiceRequestRegisterComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ServiceRequestRegisterComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ServiceRequestRegisterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
