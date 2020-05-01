import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductRegisterComponent } from './product-register.component';

describe('ProductRegisterComponent', () => {
  let component: ProductRegisterComponent;
  let fixture: ComponentFixture<ProductRegisterComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProductRegisterComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProductRegisterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
