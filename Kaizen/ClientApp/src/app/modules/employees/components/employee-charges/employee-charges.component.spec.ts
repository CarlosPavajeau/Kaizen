import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { MatDialogModule } from '@angular/material/dialog';
import { AppModule } from '@app/app.module';
import { SharedModule } from '@shared/shared.module';

import { EmployeeChargesComponent } from './employee-charges.component';

describe('EmployeeChargesComponent', () => {
  let component: EmployeeChargesComponent;
  let fixture: ComponentFixture<EmployeeChargesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ HttpClientTestingModule, AppModule, SharedModule, MatDialogModule ],
      declarations: [ EmployeeChargesComponent ]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EmployeeChargesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
