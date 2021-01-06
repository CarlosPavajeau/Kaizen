import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ProductInvoiceRegisterComponent } from './product-invoice-register.component';

xdescribe('ProductInvoiceRegisterComponent', () => {
  let component: ProductInvoiceRegisterComponent;
  let fixture: ComponentFixture<ProductInvoiceRegisterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProductInvoiceRegisterComponent ]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProductInvoiceRegisterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
