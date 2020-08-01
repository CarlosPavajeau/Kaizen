import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductInvoicesComponent } from './product-invoices.component';

describe('ProductInvoicesComponent', () => {
  let component: ProductInvoicesComponent;
  let fixture: ComponentFixture<ProductInvoicesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProductInvoicesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProductInvoicesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
