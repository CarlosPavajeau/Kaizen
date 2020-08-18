import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ServiceEditComponent } from './service-edit.component';

describe('ServiceEditComponent', () => {
  let component: ServiceEditComponent;
  let fixture: ComponentFixture<ServiceEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ServiceEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ServiceEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
