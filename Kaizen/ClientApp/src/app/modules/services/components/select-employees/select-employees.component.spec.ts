import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SelectEmployeesComponent } from './select-employees.component';

describe('SelectEmployeesComponent', () => {
  let component: SelectEmployeesComponent;
  let fixture: ComponentFixture<SelectEmployeesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SelectEmployeesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SelectEmployeesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
