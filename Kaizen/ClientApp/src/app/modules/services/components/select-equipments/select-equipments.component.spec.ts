import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SelectEquipmentsComponent } from './select-equipments.component';

describe('SelectEquipmentsComponent', () => {
  let component: SelectEquipmentsComponent;
  let fixture: ComponentFixture<SelectEquipmentsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SelectEquipmentsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SelectEquipmentsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
