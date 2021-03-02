import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ServiceTypeRegisterComponent } from './service-type-register.component';

describe('ServiceTypeRegisterComponent', () => {
  let component: ServiceTypeRegisterComponent;
  let fixture: ComponentFixture<ServiceTypeRegisterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ServiceTypeRegisterComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ServiceTypeRegisterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
