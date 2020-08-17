import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SelectProductsComponent } from './select-products.component';

describe('SelectProductsComponent', () => {
  let component: SelectProductsComponent;
  let fixture: ComponentFixture<SelectProductsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SelectProductsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SelectProductsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
