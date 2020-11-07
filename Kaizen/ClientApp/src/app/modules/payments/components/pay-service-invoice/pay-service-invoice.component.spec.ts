import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PayServiceInvoiceComponent } from './pay-service-invoice.component';

describe('PayServiceInvoiceComponent', () => {
  let component: PayServiceInvoiceComponent;
  let fixture: ComponentFixture<PayServiceInvoiceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PayServiceInvoiceComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PayServiceInvoiceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
