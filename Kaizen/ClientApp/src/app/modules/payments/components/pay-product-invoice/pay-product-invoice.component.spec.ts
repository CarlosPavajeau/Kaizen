import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PayProductInvoiceComponent } from './pay-product-invoice.component';

xdescribe('PayProductInvoiceComponent', () => {
  let component: PayProductInvoiceComponent;
  let fixture: ComponentFixture<PayProductInvoiceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PayProductInvoiceComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PayProductInvoiceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
