import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ServiceRequestProcessComponent } from './service-request-process.component';

describe('ServiceRequestProcessComponent', () => {
  let component: ServiceRequestProcessComponent;
  let fixture: ComponentFixture<ServiceRequestProcessComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ServiceRequestProcessComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ServiceRequestProcessComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
