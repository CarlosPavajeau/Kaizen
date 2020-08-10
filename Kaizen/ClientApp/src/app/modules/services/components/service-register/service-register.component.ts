import { Component, OnInit, QueryList, ViewChildren } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSelectionList, MatSelectionListChange } from '@angular/material/list';
import { Router } from '@angular/router';
import { IForm } from '@core/models/form';
import { Employee } from '@modules/employees/models/employee';
import { EmployeeService } from '@modules/employees/services/employee.service';
import { Equipment } from '@modules/inventory/equipments/models/equipment';
import { EquipmentService } from '@modules/inventory/equipments/services/equipment.service';
import { Product } from '@modules/inventory/products/models/product';
import { ProductService } from '@modules/inventory/products/services/product.service';
import { Service } from '@modules/services/models/service';
import { ServiceType } from '@modules/services/models/service-type';
import { ServiceService } from '@modules/services/services/service.service';
import { NotificationsService } from '@shared/services/notifications.service';
import { delay } from 'rxjs/operators';

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

  selectedProducts: Product[] = [];
  selectedEquipments: Equipment[] = [];
  selectedEmployees: Employee[] = [];

  serviceForm: FormGroup;
  serviceEquipmentsForm: FormGroup;
  serviceProductsForm: FormGroup;
  serviceEmployeesForm: FormGroup;

  productCode = this.formBuilder.control(null);
  equipmentCode = this.formBuilder.control(null);
  employeeCode = this.formBuilder.control(null);

  showSelectedProducts = this.formBuilder.control(false);
  showSelectedEquipments = this.formBuilder.control(false);
  showSelectedEmployees = this.formBuilder.control(false);

  @ViewChildren('productsListSelection') productsListSelection: QueryList<MatSelectionList>;
  @ViewChildren('employeeListSelection') employeeListSelection: QueryList<MatSelectionList>;
  @ViewChildren('equipmentListSelection') equipmentListSelection: QueryList<MatSelectionList>;

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
    this.initForm();
    this.loadData();
  }

  private loadData() {
    this.productService.getProducts().subscribe((products) => {
      this.products = products;
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

    this.initServiceEquipmentsForm();
    this.initServiceEmployeeForm();
    this.initServiceProductsForm();
  }

  private initServiceEquipmentsForm(): void {
    this.serviceEquipmentsForm = this.formBuilder.group({
      equipmentCodes: [ '', [ Validators.required ] ],
      showSelectedEquipments: [ false ]
    });

    this.equipmentCode.valueChanges.pipe(delay(100)).subscribe((value) => {
      if (this.selectedEquipments.length === 0 || this.equipmentListSelection.first.options === undefined) {
        return;
      }

      const selectedOptions = this.equipmentListSelection.first.options.filter((option) => {
        return this.selectedEquipments.indexOf(option.value) !== -1;
      });

      this.equipmentListSelection.first.selectedOptions.select(...selectedOptions);
    });

    this.showSelectedEquipments.valueChanges.subscribe((value) => {
      if (value) {
        this.equipmentCode.disable();
      } else {
        this.equipmentCode.enable();
      }
    });
  }

  private initServiceEmployeeForm(): void {
    this.serviceEmployeesForm = this.formBuilder.group({
      employeeCodes: [ '', [ Validators.required ] ]
    });

    this.employeeCode.valueChanges.pipe(delay(100)).subscribe((value) => {
      if (this.selectedEmployees.length === 0 || this.employeeListSelection.first.options === undefined) {
        return;
      }

      const selectedOptions = this.employeeListSelection.first.options.filter((option) => {
        return this.selectedEmployees.indexOf(option.value) !== -1;
      });

      this.employeeListSelection.first.selectedOptions.select(...selectedOptions);
    });

    this.showSelectedEmployees.valueChanges.subscribe((value) => {
      if (value) {
        this.employeeCode.disable();
      } else {
        this.employeeCode.enable();
      }
    });
  }

  private initServiceProductsForm(): void {
    this.serviceProductsForm = this.formBuilder.group({
      productCodes: [ '', [ Validators.required ] ]
    });

    this.productCode.valueChanges.pipe(delay(100)).subscribe((value) => {
      if (this.selectedProducts.length === 0 || this.productsListSelection.first.options === undefined) {
        return;
      }

      const selectedOptions = this.productsListSelection.first.options.filter((option) => {
        return this.selectedProducts.indexOf(option.value) !== -1;
      });

      this.productsListSelection.first.selectedOptions.select(...selectedOptions);
    });

    this.showSelectedProducts.valueChanges.subscribe((value) => {
      if (value) {
        this.productCode.disable();
      } else {
        this.productCode.enable();
      }
    });
  }

  onSubmit(): void {
    if (this.allFormsValid()) {
      const service = this.mapService();
      this.serviceService.saveService(service).subscribe((serviceSave) => {
        this.notificationService.showSuccessMessage(
          `El servicio ${serviceSave.name} ha sido registrado con éxito`,
          () => {
            this.router.navigateByUrl('/services');
          }
        );
      });
    }
  }

  private allFormsValid(): boolean {
    return (
      this.serviceForm.valid &&
      this.serviceProductsForm.valid &&
      this.serviceEquipmentsForm.valid &&
      this.serviceEmployeesForm.valid
    );
  }

  private mapService(): Service {
    return {
      code: this.controls['code'].value,
      name: this.controls['name'].value,
      serviceTypeId: +this.controls['serviceType'].value,
      cost: +this.controls['cost'].value,
      productCodes: this.serviceProductsForm.controls['productCodes'].value.map((p: Product) => p.code),
      equipmentCodes: this.serviceEquipmentsForm.controls['equipmentCodes'].value.map((e: Equipment) => e.code),
      employeeCodes: this.serviceEmployeesForm.controls['employeeCodes'].value.map((e: Employee) => e.id)
    };
  }

  onSelectEmployee(event: MatSelectionListChange): void {
    this.changeSelectedData<Employee>(this.selectedEmployees, event.option.value, event.option.selected);
  }

  onSelectProduct(event: MatSelectionListChange): void {
    this.changeSelectedData<Product>(this.selectedProducts, event.option.value, event.option.selected);
  }

  onSelectEquipment(event: MatSelectionListChange): void {
    this.changeSelectedData<Equipment>(this.selectedEquipments, event.option.value, event.option.selected);
  }

  private changeSelectedData<T>(data: T[], value: T, add_data: boolean): void {
    if (add_data) {
      data.push(value);
    } else {
      // Delete data
      const index = data.indexOf(value);
      if (index !== -1) {
        data.splice(index, 1);
      }
    }
  }
}
