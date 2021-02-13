import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { GoogleMapsModule } from '@angular/google-maps';
import { SharedModule } from '@shared/shared.module';

import { EmployeeMapComponent } from './employee-map.component';

describe('EmployeeMapComponent', () => {
  let component: EmployeeMapComponent;
  let fixture: ComponentFixture<EmployeeMapComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ HttpClientTestingModule, SharedModule, GoogleMapsModule ],
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
