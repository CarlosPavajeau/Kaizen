import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { IForm } from '@core/models/form';
import { AbstractControl, FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { COMMA, ENTER } from '@angular/cdk/keycodes';
import { ProductService } from '@modules/inventory/products/services/product.service';
import { ProductExistsValidator } from '@shared/validators/product-exists-validator';
import { Observable, of } from 'rxjs';
import { MatAutocomplete, MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';
import { startWith, map } from 'rxjs/operators';
import { MatChipInputEvent } from '@angular/material/chips';
import { MONTHS_NAMES } from '@global/months';
import { UploadDownloadService } from '@app/core/services/upload-download.service';
import { HttpEventType, HttpEvent } from '@angular/common/http';
import { FileResponse } from '@app/core/models/file-response';
import { Product } from '../../models/product';
import { Router } from '@angular/router';
import { NotificationsService } from '@app/shared/services/notifications.service';

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
	uploading: boolean = false;
	uploadP: number;

	public get controls(): { [key: string]: AbstractControl } {
		return this.productForm.controls;
	}

	public get documents_controls(): { [key: string]: AbstractControl } {
		return this.productDocumentsForm.controls;
	}

	constructor(
		private productService: ProductService,
		private uploadDownloadService: UploadDownloadService,
		private formBuilder: FormBuilder,
		private productExistsValidator: ProductExistsValidator,
		private notificationService: NotificationsService,
		private router: Router
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
			name: [ '', [ Validators.required, Validators.maxLength(40) ] ],
			description: [ '', [ Validators.required, Validators.maxLength(300) ] ],
			amount: [ '', [ Validators.required ] ],
			presentation: [ '', [ Validators.required ] ],
			price: [ '', [ Validators.required ] ],
			applicationMonths: [ '' ]
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

	onSubmit() {
		if (this.productForm.valid && this.productDocumentsForm.valid) {
			this.uploadDocuments().subscribe((result) => {
				if (result.type == HttpEventType.UploadProgress) {
					this.uploadP = Math.round(100 * (result.loaded / result.total));
				} else if (result.type == HttpEventType.Response) {
					const fileNames = result.body;
					this.uploading = false;
					const product = this.mapProduct(fileNames);

					this.productService.saveProduct(product).subscribe((productSave) => {
						this.notificationService.add(`Producto ${productSave.name} registrado`, 'Ok');
						setTimeout(() => {
							this.router.navigateByUrl('/inventory/products');
						}, 2000);
					});
				}
			});
		}
	}

	private mapProduct(fileNames: FileResponse[]): Product {
		return {
			code: this.controls['code'].value,
			name: this.controls['name'].value,
			amount: +this.controls['amount'].value,
			presentation: this.controls['presentation'].value,
			price: +this.controls['price'].value,
			description: this.controls['description'].value,
			dataSheet: fileNames[0].fileName,
			healthRegister: fileNames[1].fileName,
			safetySheet: fileNames[2].fileName,
			emergencyCard: fileNames[3].fileName
		};
	}

	private uploadDocuments() {
		const dataSheet = <File>this.documents_controls['dataSheet'].value.files[0];
		const healtRegister = <File>this.documents_controls['healthRegister'].value.files[0];
		const safetySheet = <File>this.documents_controls['safetySheet'].value.files[0];
		const emergencyCard = <File>this.documents_controls['emergencyCard'].value.files[0];

		const files = [ dataSheet, healtRegister, safetySheet, emergencyCard ];
		this.uploading = true;

		return this.uploadDownloadService.uploadFiles(files);
	}

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
