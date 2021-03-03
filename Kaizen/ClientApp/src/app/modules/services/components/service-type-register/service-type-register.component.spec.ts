import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { AppModule } from '@app/app.module';
import { SharedModule } from '@shared/shared.module';

import { ServiceTypeRegisterComponent } from './service-type-register.component';

describe('ServiceTypeRegisterComponent', () => {
  let component: ServiceTypeRegisterComponent;
  let fixture: ComponentFixture<ServiceTypeRegisterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ AppModule, MatDialogModule, ReactiveFormsModule, SharedModule ],
      declarations: [ ServiceTypeRegisterComponent ],
      providers: [ { provide: MatDialogRef, useValue: {} } ]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ServiceTypeRegisterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
