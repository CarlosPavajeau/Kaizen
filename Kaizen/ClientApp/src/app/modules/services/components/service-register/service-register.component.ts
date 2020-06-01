import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { ProductService } from '@modules/inventory/products/services/product.service';
import { EquipmentService } from '@modules/inventory/equipments/services/equipment.service';
import { ServiceService } from '@modules/services/services/service.service';
import { Product } from '@modules/inventory/products/models/product';
import { Equipment } from '@modules/inventory/equipments/models/equipment';
import { FormBuilder, AbstractControl, FormGroup, Validators } from '@angular/forms';
import { IForm } from '@core/models/form';
import { Employee } from '@modules/employees/models/employee';
import { EmployeeService } from '@modules/employees/services/employee.service';
import { ServiceType } from '@modules/services/models/service-type';
import { Observable } from 'rxjs';
import { startWith, map } from 'rxjs/operators';
import { MatAutocompleteSelectedEvent, MatAutocomplete } from '@angular/material/autocomplete';
import { MatChipInputEvent } from '@angular/material/chips';
import { ENTER, COMMA } from '@angular/cdk/keycodes';
import { MatAutocompleteChipListInputComponent } from '@shared/components/mat-autocomplete-chip-list-input/mat-autocomplete-chip-list-input.component';

@Component({
	selector: 'app-service-register',
	templateUrl: './service-register.component.html',
	styleUrls: [ './service-register.component.css' ]
})
export class ServiceRegisterComponent implements OnInit, IForm {
	products: Product[] = [];
	equipments: Equipment[] = [];
	employees: Employee[] = [];
	serviceTypes: ServiceType[] = [];

	filteredProducts: Observable<Product[]>;
	filteredEquipments: Observable<Equipment[]>;
	filteredEmployees: Observable<Employee[]>;

	selectedProducts: Product[] = [];
	selectedEquipments: Equipment[] = [];
	selectedEmployees: Employee[] = [];

	serviceForm: FormGroup;
	serviceEquipmentsForm: FormGroup;
	serviceProductsForm: FormGroup;
	serviceEmployeesForm: FormGroup;

	separatorKeysCodes: number[] = [ ENTER, COMMA ];

	@ViewChild('productInput') productInput: ElementRef<HTMLInputElement>;
	@ViewChild('autoProducts') autoProducts: MatAutocomplete;

	@ViewChild('equipmentInput') equipmentInput: ElementRef<HTMLInputElement>;
	@ViewChild('autoEquipments') autoEquipments: MatAutocomplete;

	@ViewChild('employeeInput') employeeInput: ElementRef<HTMLInputElement>;
	@ViewChild('autoEmployee') autoEmployee: MatAutocomplete;

	@ViewChild('productAuto') productAuto: MatAutocompleteChipListInputComponent<Product>;

	public get controls(): { [key: string]: AbstractControl } {
		return this.serviceForm.controls;
	}

	constructor(
		private productService: ProductService,
		private equipmentService: EquipmentService,
		private employeeService: EmployeeService,
		private serviceService: ServiceService,
		private formBuilder: FormBuilder
	) {}

	ngOnInit(): void {
		this.loadData();
		this.initForm();

		this.filteredProducts = this.serviceProductsForm.controls['productCodes'].valueChanges.pipe(
			startWith(null),
			map(
				(productCode: string | null) =>

						productCode ? this.filterProducts(productCode) :
						this.products
			)
		);

		this.filteredEquipments = this.serviceEquipmentsForm.controls['equipmentCodes'].valueChanges.pipe(
			startWith(null),
			map(
				(equipmentCode: string | null) =>

						equipmentCode ? this.filterEquipemts(equipmentCode) :
						this.equipments
			)
		);

		this.filteredEmployees = this.serviceEmployeesForm.controls['employeeCodes'].valueChanges.pipe(
			startWith(null),
			map(
				(employeeCode: string | null) =>

						employeeCode ? this.filterEmployees(employeeCode) :
						this.employees
			)
		);
	}

	private filterProducts(value: string): Product[] {
		if (typeof value == 'string') {
			return this.products.filter((product) => product.code.toLowerCase().indexOf(value.toLowerCase()) != -1);
		} else {
			return this.products.slice();
		}
	}

	private filterEquipemts(value: string): Equipment[] {
		if (typeof value == 'string') {
			return this.equipments.filter(
				(equipment) => equipment.code.toLowerCase().indexOf(value.toLowerCase()) != -1
			);
		} else {
			return this.equipments.slice();
		}
	}

	private filterEmployees(value: string): Employee[] {
		if (typeof value == 'string') {
			return this.employees.filter((employee) => employee.id.toLowerCase().indexOf(value.toLowerCase()) != -1);
		} else {
			return this.employees.slice();
		}
	}

	filterProductData(dataCode: string | Product, data: Product): boolean {
		if (typeof dataCode == 'string') {
			return data.code == dataCode;
		} else {
			return dataCode.code == data.code;
		}
	}

	productStr(data: Product): string {
		return `${data.code} - ${data.name}`;
	}

	private loadData() {
		this.productService.getProducts().subscribe((products) => {
			this.products = products;
			this.productAuto.initialData = this.products;
		});
		this.equipmentService.getEquipments().subscribe((equipments) => {
			this.equipments = equipments;
		});
		this.employeeService.getEmployees().subscribe((employees) => {
			this.employees = employees.filter((em) => [ 6, 7, 8 ].includes(em.chargeId));
		});
		this.serviceService.getServiceTypes().subscribe((serviceTypes) => {
			this.serviceTypes = serviceTypes;
		});
	}

	initForm(): void {
		this.serviceForm = this.formBuilder.group({
			code: [ '', [ Validators.required ] ],
			name: [ '', [ Validators.required ] ],
			serviceType: [ '', [ Validators.required ] ],
			cost: [ '', [ Validators.required ] ]
		});

		this.serviceEquipmentsForm = this.formBuilder.group({
			equipmentCodes: [ '', [ Validators.required ] ]
		});

		this.serviceProductsForm = this.formBuilder.group({
			productCodes: [ '', [ Validators.required ] ]
		});

		this.serviceEmployeesForm = this.formBuilder.group({
			employeeCodes: [ '', [ Validators.required ] ],
			employeeCodes2: [ '2', [ Validators.required ] ]
		});
	}

	addProduct(event: MatChipInputEvent): void {
		const input = event.input;
		const value = event.value;

		const product = this.products.find((p) => p.code == value);

		if (product) {
			this.selectedProducts.push(product);
			this.products = this.products.filter((p) => p.code != product.code);

			if (input) {
				input.value = '';
				const codes = Array.from(this.selectedProducts, (p) => p.code);
				this.serviceProductsForm.controls['productCodes'].setValue(codes);
			}
		}
	}

	selectProduct(event: MatAutocompleteSelectedEvent): void {
		const value = event.option.value;

		this.selectedProducts.push(value);
		this.products = this.products.filter((p) => p.code != value.code);
		this.productInput.nativeElement.value = '';
		const codes = Array.from(this.selectedProducts, (p) => p.code);
		this.serviceProductsForm.controls['productCodes'].setValue(codes);
	}

	deleteProduct(product: Product): void {
		this.selectedProducts = this.selectedProducts.filter((p) => p != product);
		this.products.push(product);
	}

	addEmployee(event: MatChipInputEvent): void {
		const input = event.input;
		const value = event.value;

		const employee = this.employees.find((p) => p.id == value);

		if (employee) {
			this.selectedEmployees.push(employee);
			this.employees = this.employees.filter((p) => p.id != employee.id);

			if (input) {
				input.value = '';
				const codes = Array.from(this.selectedEmployees, (p) => p.id);
				this.serviceEmployeesForm.controls['employeeCodes'].setValue(codes);
			}
		}
	}

	selectedEmployee(event: MatAutocompleteSelectedEvent): void {
		const value = event.option.value;

		this.selectedEmployees.push(value);
		this.employees = this.employees.filter((p) => p.id != value.id);
		this.equipmentInput.nativeElement.value = '';
		const codes = Array.from(this.selectedEmployees, (p) => p.id);
		this.serviceEmployeesForm.controls['employeeCodes'].setValue(codes);
	}

	deleteEmployee(employee: Employee): void {
		this.selectedEmployees = this.selectedEmployees.filter((p) => p.id != employee.id);
		this.employees.push(employee);
	}

	addEquipment(event: MatChipInputEvent): void {
		const input = event.input;
		const value = event.value;

		const equipment = this.equipments.find((p) => p.code == value);

		if (equipment) {
			this.selectedEquipments.push(equipment);
			this.equipments = this.equipments.filter((p) => p.code != equipment.code);

			if (input) {
				input.value = '';
				const codes = Array.from(this.selectedProducts, (p) => p.code);
				this.serviceEquipmentsForm.controls['equipmentCodes'].setValue(codes);
			}
		}
	}

	selectedEquipment(event: MatAutocompleteSelectedEvent): void {
		const value = event.option.value;

		this.selectedEquipments.push(value);
		this.equipments = this.equipments.filter((p) => p.code != value.code);
		this.equipmentInput.nativeElement.value = '';
		const codes = Array.from(this.selectedEquipments, (p) => p.code);
		this.serviceEquipmentsForm.controls['equipmentCodes'].setValue(codes);
	}

	deleteEquipment(equipment: Equipment): void {
		this.selectedEquipments = this.selectedEquipments.filter((p) => p.code != equipment.code);
		this.equipments.push(equipment);
	}
}
