import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductInvoiceDetailComponent } from './product-invoice-detail.component';

xdescribe('ProductInvoiceDetailComponent', () => {
  let component: ProductInvoiceDetailComponent;
  let fixture: ComponentFixture<ProductInvoiceDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProductInvoiceDetailComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProductInvoiceDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
