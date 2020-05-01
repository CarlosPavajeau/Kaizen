import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EquipmentRegisterComponent } from './equipment-register.component';

describe('EquipmentRegisterComponent', () => {
  let component: EquipmentRegisterComponent;
  let fixture: ComponentFixture<EquipmentRegisterComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EquipmentRegisterComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EquipmentRegisterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
