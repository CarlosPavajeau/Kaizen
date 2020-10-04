import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { AppModule } from '@app/app.module';
import { SelectDateModalComponent } from './select-date-modal.component';

describe('SelectDateModalComponent', () => {
  let component: SelectDateModalComponent;
  let fixture: ComponentFixture<SelectDateModalComponent>;

  beforeEach(
    async(() => {
      TestBed.configureTestingModule({
        declarations: [ SelectDateModalComponent ],
        imports: [ AppModule, MatDialogModule, ReactiveFormsModule ],
        providers: [ { provide: MatDialogRef, useValue: {} } ]
      }).compileComponents();
    })
  );

  beforeEach(() => {
    fixture = TestBed.createComponent(SelectDateModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
