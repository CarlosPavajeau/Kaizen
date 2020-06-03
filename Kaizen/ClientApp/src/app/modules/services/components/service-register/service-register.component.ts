import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Component, OnInit, ViewChild } from '@angular/core';
import { Employee } from '@modules/employees/models/employee';
import { EmployeeService } from '@modules/employees/services/employee.service';
import { Equipment } from '@modules/inventory/equipments/models/equipment';
import { EquipmentService } from '@modules/inventory/equipments/services/equipment.service';
import { IForm } from '@core/models/form';
import { MatAutocompleteChipListInputComponent } from '@shared/components/mat-autocomplete-chip-list-input/mat-autocomplete-chip-list-input.component';
import { NotificationsService } from '@shared/services/notifications.service';
import { Product } from '@modules/inventory/products/models/product';
import { ProductService } from '@modules/inventory/products/services/product.service';
import { Service } from '@modules/services/models/service';
import { ServiceService } from '@modules/services/services/service.service';
import { ServiceType } from '@modules/services/models/service-type';
import { Router } from '@angular/router';

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

	serviceForm: FormGroup;
	serviceEquipmentsForm: FormGroup;
	serviceProductsForm: FormGroup;
	serviceEmployeesForm: FormGroup;

	@ViewChild('equipmentAuto') equipmentAuto: MatAutocompleteChipListInputComponent<Equipment>;
	@ViewChild('productAuto') productAuto: MatAutocompleteChipListInputComponent<Product>;
	@ViewChild('employeeAuto') employeeAuto: MatAutocompleteChipListInputComponent<Employee>;

	public get controls(): { [key: string]: AbstractControl } {
		return this.serviceForm.controls;
	}

	constructor(
		private productService: ProductService,
		private equipmentService: EquipmentService,
		private employeeService: EmployeeService,
		private serviceService: ServiceService,
		private formBuilder: FormBuilder,
		private notificationService: NotificationsService,
		private router: Router
	) {}

	ngOnInit(): void {
		this.loadData();
		this.initForm();
	}

	filterProduct(dataCode: string | Product, data: Product): boolean {
		if (typeof dataCode == 'string') {
			return data.code.toLowerCase().indexOf(dataCode.toLowerCase()) != -1;
		} else {
			return data.code.toLowerCase().indexOf(dataCode.code.toLowerCase()) != -1;
		}
	}

	findProduct(code: string | number, data: Product): boolean {
		if (typeof code == 'string') {
			return code.toLowerCase() == data.code.toLowerCase();
		} else {
			return false;
		}
	}

	productStr(data: Product): string {
		return `${data.code} - ${data.name}`;
	}

	filterEquipment(dataCode: string | Equipment, data: Equipment): boolean {
		if (typeof dataCode == 'string') {
			return data.code.toLowerCase().indexOf(dataCode.toLowerCase()) != -1;
		} else {
			return data.code.toLowerCase().indexOf(dataCode.code.toLowerCase()) != -1;
		}
	}

	findEquipment(code: string | number, data: Equipment): boolean {
		if (typeof code == 'string') {
			return code.toLowerCase() == data.code.toLowerCase();
		} else {
			return false;
		}
	}

	equipmentStr(data: Equipment): string {
		return `${data.code} - ${data.name}`;
	}

	filterEmployee(dataCode: string | Employee, data: Employee): boolean {
		if (typeof dataCode == 'string') {
			return data.id.toLowerCase().indexOf(dataCode.toLowerCase()) != -1;
		} else {
			return data.id.toLowerCase().indexOf(dataCode.id.toLowerCase()) != -1;
		}
	}

	findEmployee(code: string | number, data: Employee): boolean {
		if (typeof code == 'string') {
			return code.toLowerCase() == data.id.toLowerCase();
		} else {
			return false;
		}
	}

	employeeStr(data: Employee): string {
		return `${data.id} - ${data.lastName} ${data.firstName}`;
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
			employeeCodes: [ '', [ Validators.required ] ]
		});
	}

	onSubmit(): void {
		if (this.serviceForm.valid) {
			const service = this.mapService();
			this.serviceService.saveService(service).subscribe((serviceSave) => {
				this.notificationService.add(`El servicio ${serviceSave.name} ha sido registrado`, 'Ok');
				this.router.navigateByUrl('/services');
			});
		}
	}

	private mapService(): Service {
		return {
			code: this.controls['code'].value,
			name: this.controls['name'].value,
			serviceTypeId: +this.controls['serviceType'].value,
			cost: +this.controls['cost'].value,
			productCodes: this.productAuto.value.map((p) => p.code),
			equipmentCodes: this.equipmentAuto.value.map((e) => e.code),
			employeeCodes: this.employeeAuto.value.map((em) => em.id)
		};
	}
}
