import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { IForm } from '@core/models/form';
import { AbstractControl, FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { COMMA, ENTER } from '@angular/cdk/keycodes';
import { ProductService } from '@modules/inventory/products/services/product.service';
import { ProductExistsValidator } from '@shared/validators/product-exists-validator';
import { Observable } from 'rxjs';
import { MatAutocomplete, MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';
import { startWith, map } from 'rxjs/operators';
import { MatChipInputEvent } from '@angular/material/chips';
import { MONTHS_NAMES } from '@global/months';

@Component({
	selector: 'app-product-register',
	templateUrl: './product-register.component.html',
	styleUrls: [ './product-register.component.css' ]
})
export class ProductRegisterComponent implements OnInit, IForm {
	allMonths: string[];
	visible = true;
	selectable = true;
	removable = true;
	separatorKeysCodes: number[] = [ ENTER, COMMA ];
	filteredMonths: Observable<string[]>;
	applicationMonthsArray: string[] = [];
	@ViewChild('monthInput') monthInput: ElementRef<HTMLInputElement>;
	@ViewChild('auto') matAutocomplete: MatAutocomplete;
	productForm: FormGroup;
	productDocumentsForm: FormGroup;

	public get controls(): { [key: string]: AbstractControl } {
		return this.productForm.controls;
	}

	public get documents_controls(): { [key: string]: AbstractControl } {
		return this.productDocumentsForm.controls;
	}

	constructor(
		private productService: ProductService,
		private formBuilder: FormBuilder,
		private productExistsValidator: ProductExistsValidator
	) {}

	ngOnInit(): void {
		this.initForm();
		this.initProductDocumentsForm();

		this.allMonths = MONTHS_NAMES;
		this.filteredMonths = this.controls['applicationMonths'].valueChanges.pipe(
			startWith(null),
			map(
				(month: string | null) =>

						month ? this._filter(month) :
						this.allMonths.slice()
			)
		);
	}

	initForm(): void {
		this.productForm = this.formBuilder.group({
			code: [
				'',
				{
					validators: [ Validators.required ],
					asyncValidators: [ this.productExistsValidator.validate.bind(this.productExistsValidator) ]
				}
			],
			amount: [ '', [ Validators.required ] ],
			presentation: [ '', [ Validators.required ] ],
			price: [ '', [ Validators.required ] ],
			applicationMonths: [ '', [ Validators.required ] ]
		});
	}

	private initProductDocumentsForm(): void {
		this.productDocumentsForm = this.formBuilder.group({
			dataSheet: [ '', [ Validators.required ] ],
			healthRegister: [ '', [ Validators.required ] ],
			safetySheet: [ '', [ Validators.required ] ],
			emergencyCard: [ '', [ Validators.required ] ]
		});
	}

	onSubmit() {}

	addMonth(event: MatChipInputEvent): void {
		const input = event.input;
		const value = event.value;

		if ((value || '').trim()) {
			this.applicationMonthsArray.push(value.trim());
		}

		if (input) {
			input.value = '';
		}

		this.controls['applicationMonths'].setValue(null);
	}

	removeMonth(month: string) {
		const index = this.applicationMonthsArray.indexOf(month);
		if (index >= 0) {
			this.applicationMonthsArray.splice(index, 1);
		}
	}

	selectedMonth(event: MatAutocompleteSelectedEvent): void {
		this.applicationMonthsArray.push(event.option.viewValue);
		this.monthInput.nativeElement.value = '';
		this.controls['applicationMonths'].setValue(null);
	}

	private _filter(value: string): string[] {
		const filterValue = value.toLowerCase();

		return this.allMonths.filter((month) => month.toLowerCase().indexOf(filterValue) === 0);
	}
}
