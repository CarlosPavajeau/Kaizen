import { Component, OnInit, Input, ElementRef, ViewChild, Optional, Self } from '@angular/core';
import { Observable, Subject, of } from 'rxjs';
import { MatAutocomplete, MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';
import { MatFormFieldControl } from '@angular/material/form-field';
import { NgControl, ControlValueAccessor, NgForm, FormGroupDirective, FormControlName } from '@angular/forms';
import { ENTER, COMMA } from '@angular/cdk/keycodes';
import { map, startWith } from 'rxjs/operators';
import { MatChipEvent, MatChipInputEvent } from '@angular/material/chips';

@Component({
	selector: 'mat-autocomplete-chip-list-input',
	templateUrl: './mat-autocomplete-chip-list-input.component.html',
	styleUrls: [ './mat-autocomplete-chip-list-input.component.css' ],
	providers: [ { provide: MatFormFieldControl, useExisting: MatAutocompleteChipListInputComponent } ]
})
export class MatAutocompleteChipListInputComponent<T>
	implements OnInit, MatFormFieldControl<T[]>, ControlValueAccessor {
	@Input() initialData: T[] = [];
	@Input() filterFunction: (code: string | T, value: T) => boolean;
	@Input() dataStrFunction: (data: T) => string;
	filteredData: Observable<T[]>;
	selectedData: T[] = [];

	@ViewChild('autoInput') autoInput: ElementRef<HTMLInputElement>;
	@ViewChild('autoComplete') autoComplete: MatAutocomplete;

	separatorKeysCodes: number[] = [ ENTER, COMMA ];

	private _placeholder: string;

	constructor(
		@Optional()
		@Self()
		public ngControl: NgControl,
		@Optional() public _parentForm: NgForm,
		@Optional() public _parentFormGroup: FormGroupDirective
	) {
		ngControl.valueAccessor = this;
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

	stateChanges: Subject<void> = new Subject<void>();
	id: string;
	focused: boolean = false;
	empty: boolean;
	shouldLabelFloat: boolean;
	required: boolean;
	disabled: boolean;
	errorState: boolean;
	controlType?: string;
	autofilled?: boolean;

	ngOnInit(): void {
		this.filteredData = this.ngControl.control.valueChanges.pipe(
			startWith(null),
			map(
				(dataCode: string | null) =>

						dataCode ? this.filterData(dataCode) :
						this.initialData
			)
		);

		this.ngControl.valueChanges.subscribe((s) => {
			console.log(s);
		});
	}

	filterData(dataCode: string): T[] {
		return this.initialData.filter((data) => this.filterFunction(dataCode, data));
	}

	setDescribedByIds(ids: string[]): void {}
	onContainerClick(event: MouseEvent): void {}

	writeValue(obj: any): void {}
	registerOnChange(fn: any): void {}
	registerOnTouched(fn: any): void {}
	setDisabledState?(isDisabled: boolean): void {}

	dataStr(data: T): string {
		return this.dataStrFunction(data);
	}

	addData(event: MatChipInputEvent): void {
		const input = event.input;
		const value = event.value;
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
	}
}
