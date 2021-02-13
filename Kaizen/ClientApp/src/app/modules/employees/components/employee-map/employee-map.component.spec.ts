import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployeeMapComponent } from './employee-map.component';

describe('EmployeeMapComponent', () => {
  let component: EmployeeMapComponent;
  let fixture: ComponentFixture<EmployeeMapComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EmployeeMapComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EmployeeMapComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
