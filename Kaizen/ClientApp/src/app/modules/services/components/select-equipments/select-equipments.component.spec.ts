import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { AppModule } from '@app/app.module';
import { SharedModule } from '@app/shared/shared.module';
import { SelectEquipmentsComponent } from './select-equipments.component';

describe('SelectEquipmentsComponent', () => {
  let component: SelectEquipmentsComponent;
  let fixture: ComponentFixture<SelectEquipmentsComponent>;

  beforeEach(
    waitForAsync(() => {
      TestBed.configureTestingModule({
        imports: [ HttpClientTestingModule, RouterTestingModule, SharedModule, AppModule ],
        declarations: [ SelectEquipmentsComponent ]
      }).compileComponents();
    })
  );

  beforeEach(() => {
    fixture = TestBed.createComponent(SelectEquipmentsComponent);
    component = fixture.componentInstance;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
