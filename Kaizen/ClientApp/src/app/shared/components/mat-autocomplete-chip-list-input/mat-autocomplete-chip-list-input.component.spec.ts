import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MatAutocompleteChipListInputComponent } from './mat-autocomplete-chip-list-input.component';

describe('MatAutocompleteChipListInputComponent', () => {
  let component: MatAutocompleteChipListInputComponent;
  let fixture: ComponentFixture<MatAutocompleteChipListInputComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MatAutocompleteChipListInputComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MatAutocompleteChipListInputComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
