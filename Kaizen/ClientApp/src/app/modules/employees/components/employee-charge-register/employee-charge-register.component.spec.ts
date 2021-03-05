import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { AppModule } from '@app/app.module';
import { SharedModule } from '@shared/shared.module';

import { EmployeeChargeRegisterComponent } from './employee-charge-register.component';

describe('EmployeeChargeRegisterComponent', () => {
  let component: EmployeeChargeRegisterComponent;
  let fixture: ComponentFixture<EmployeeChargeRegisterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ AppModule, MatDialogModule, ReactiveFormsModule, SharedModule ],
      declarations: [ EmployeeChargeRegisterComponent ],
      providers: [ { provide: MatDialogRef, useValue: {} } ]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EmployeeChargeRegisterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
