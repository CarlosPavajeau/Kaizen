import { Component, OnInit, Input, ElementRef, ViewChild, Optional, Self, OnDestroy, HostBinding } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { MatAutocomplete, MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';
import { MatFormFieldControl } from '@angular/material/form-field';
import { NgControl, ControlValueAccessor, NgForm, FormGroupDirective, FormBuilder, FormGroup } from '@angular/forms';
import { ENTER, COMMA } from '@angular/cdk/keycodes';
import { map, startWith } from 'rxjs/operators';
import { MatChipInputEvent } from '@angular/material/chips';
import { FocusMonitor } from '@angular/cdk/a11y';
import { coerceBooleanProperty } from '@angular/cdk/coercion';

@Component({
  selector: 'app-mat-autocomplete-chip-list-input',
  templateUrl: './mat-autocomplete-chip-list-input.component.html',
  styleUrls: [ './mat-autocomplete-chip-list-input.component.css' ],
  providers: [ { provide: MatFormFieldControl, useExisting: MatAutocompleteChipListInputComponent } ]
})
export class MatAutocompleteChipListInputComponent<T>
  implements OnInit, OnDestroy, MatFormFieldControl<T[]>, ControlValueAccessor {
  static nextId = 0;

  @Input() initialData: T[] = [];
  @Input() filterFunction: (code: string | T, value: T) => boolean;
  @Input() findFunction: (code: string | number, value: T) => boolean;
  @Input() dataStrFunction: (data: T) => string;
  @Input() label: string;
  @Input() hint: string;

  filteredData: Observable<T[]>;
  selectedData: T[] = [];

  @ViewChild('autoInput') autoInput: ElementRef<HTMLInputElement>;
  @ViewChild('autoComplete') autoComplete: MatAutocomplete;

  separatorKeysCodes: number[] = [ ENTER, COMMA ];

  private _placeholder: string;
  formGroup: FormGroup;

  errorState = false;
  controlType = 'mat-autocomplete-chip-list-input';
  autofilled?: boolean;

  @HostBinding('attr.aria-describedby') describeBy = '';

  stateChanges: Subject<void> = new Subject<void>();

  @HostBinding() id = `mat-autocomplete-chip-list-input-${MatAutocompleteChipListInputComponent.nextId++}`;

  focused = false;
  private _required = false;
  private _disabled = false;

  constructor(
    @Optional()
    @Self()
    public ngControl: NgControl,
    @Optional() public _parentForm: NgForm,
    @Optional() public _parentFormGroup: FormGroupDirective,
    private formBuilder: FormBuilder,
    private fm: FocusMonitor,
    private elRef: ElementRef<HTMLElement>
  ) {
    if (this.ngControl != null) {
      ngControl.valueAccessor = this;
    }

    fm.monitor(elRef.nativeElement, true).subscribe((origin) => {
      this.focused = !!origin;
      this.stateChanges.next();
    });
  }
  ngOnDestroy(): void {
    this.stateChanges.complete();
    this.fm.stopMonitoring(this.elRef.nativeElement);
  }

  @Input()
  get placeholder() {
    return this._placeholder;
  }
  set placeholder(plh) {
    this._placeholder = plh;
    this.stateChanges.next();
  }

  @Input()
  get value(): T[] | null {
    return this.selectedData;
  }

  set value(data: T[] | null) {
    this.selectedData = data;
    this.stateChanges.next();
  }

  get empty(): boolean {
    return this.selectedData.length === 0;
  }

  @HostBinding('class.floating')
  get shouldLabelFloat(): boolean {
    return this.focused || !this.empty || this.formGroup.controls['autoControl'].value;
  }

  @Input()
  get required(): boolean {
    return this._required;
  }

  set required(req) {
    this._required = coerceBooleanProperty(req);
    this.stateChanges.next();
  }

  @Input()
  get disabled(): boolean {
    return this._disabled;
  }

  set disabled(value: boolean) {
    this._disabled = coerceBooleanProperty(value);


      this._disabled ? this.formGroup.disable() :
      this.formGroup.enable();
    this.stateChanges.next();
  }

  setDescribedByIds(ids: string[]): void {
    this.describeBy = ids.join(' ');
  }

  onContainerClick(event: MouseEvent): void {
    if ((event.target as Element).tagName.toLowerCase() !== 'input') {
      this.elRef.nativeElement.querySelector('input').focus();
    }
  }

  writeValue(obj: any): void {}
  registerOnChange(fn: any): void {}
  registerOnTouched(fn: any): void {}
  setDisabledState?(isDisabled: boolean): void {}

  ngOnInit(): void {
    this.formGroup = this.formBuilder.group({
      autoControl: ''
    });

    this.filteredData = this.formGroup.controls['autoControl'].valueChanges.pipe(
      startWith(<string>null),
      map((dataCode: string | null) => {
        const data =
          dataCode ? this.filterData(dataCode) :
          this.initialData.slice();
        return data;
      })
    );
  }

  filterData(dataCode: string): T[] {
    if (typeof dataCode === 'string') {
      return this.initialData.filter((data) => this.filterFunction(dataCode, data));
    } else {
      return this.initialData.slice();
    }
  }

  dataStr(data: T): string {
    return this.dataStrFunction(data);
  }

  addData(event: MatChipInputEvent): void {
    const input = event.input;
    const value = event.value;

    const data = this.initialData.find((d) => this.findFunction(value, d));

    if (data) {
      this.addSelectData(data);

      if (input) {
        input.value = '';
      }
    }
  }

  selectData(event: MatAutocompleteSelectedEvent): void {
    const value = event.option.value;

    this.addSelectData(value);
    this.autoInput.nativeElement.value = '';
  }

  removeData(data: T): void {
    this.selectedData = this.selectedData.filter((s) => !this.filterFunction(s, data));
    this.initialData.push(data);
  }

  private addSelectData(data: T) {
    this.selectedData.push(data);
    this.initialData = this.initialData.filter((i) => !this.filterFunction(i, data));
    this.stateChanges.next(null);
  }
}
