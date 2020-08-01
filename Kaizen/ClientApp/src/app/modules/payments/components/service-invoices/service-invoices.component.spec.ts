import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ServiceInvoicesComponent } from './service-invoices.component';

describe('ServiceInvoicesComponent', () => {
  let component: ServiceInvoicesComponent;
  let fixture: ComponentFixture<ServiceInvoicesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ServiceInvoicesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ServiceInvoicesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
