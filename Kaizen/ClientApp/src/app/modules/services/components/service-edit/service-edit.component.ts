import { Component, OnInit, ViewChild } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatTabChangeEvent } from '@angular/material/tabs';
import { ActivatedRoute } from '@angular/router';
import { IForm } from '@core/models/form';
import { SelectEmployeesComponent } from '@modules/services/components/select-employees/select-employees.component';
import { SelectEquipmentsComponent } from '@modules/services/components/select-equipments/select-equipments.component';
import { SelectProductsComponent } from '@modules/services/components/select-products/select-products.component';
import { Service } from '@modules/services/models/service';
import { ServiceType } from '@modules/services/models/service-type';
import { ServiceService } from '@modules/services/services/service.service';
import { NotificationsService } from '@shared/services/notifications.service';

@Component({
  selector: 'app-service-edit',
  templateUrl: './service-edit.component.html',
  styleUrls: [ './service-edit.component.scss' ]
})
export class ServiceEditComponent implements OnInit, IForm {
  service: Service;
  serviceTypes: ServiceType[];

  basicDataForm: FormGroup;

  @ViewChild('selectEquipments') selectEquipments: SelectEquipmentsComponent;
  @ViewChild('selectProducts') selectProducts: SelectProductsComponent;
  @ViewChild('selectEmployees') selectEmployees: SelectEmployeesComponent;

  updatingService = false;

  get controls(): { [key: string]: AbstractControl } {
    return this.basicDataForm.controls;
  }

  constructor(
    private serviceService: ServiceService,
    private activatedRoute: ActivatedRoute,
    private formBuilder: FormBuilder,
    private notificationsService: NotificationsService
  ) {}

  ngOnInit(): void {
    this.initForm();
    this.loadData();
  }

  private loadData(): void {
    const code = this.activatedRoute.snapshot.paramMap.get('code');
    this.serviceService.getService(code).subscribe((service) => {
      this.service = service;
      this.afterLoadService();
    });

    this.serviceService.getServiceTypes().subscribe((serviceTypes) => {
      this.serviceTypes = serviceTypes;
    });
  }

  private afterLoadService(): void {
    this.basicDataForm.setValue({
      name: this.service.name,
      serviceType: this.service.serviceTypeId,
      cost: this.service.cost
    });

    console.log(this.selectEquipments);
  }

  equipmentTab(): void {
    console.log('Equipment');
  }

  selectedTabChange(event: MatTabChangeEvent): void {
    switch (event.index) {
      case 1:
        const selectedEquipments = this.selectEquipments.equipments.filter((e) => {
          return this.service.equipments.map((eq) => eq.code).indexOf(e.code) !== -1;
        });
        this.selectEquipments.setValue(selectedEquipments);
        break;
      case 2:
        const selectedProducts = this.selectProducts.products.filter((p) => {
          return this.service.products.map((pro) => pro.code).indexOf(p.code) !== -1;
        });
        this.selectProducts.setValue(selectedProducts);
        break;
      case 3:
        const selectedEmployees = this.selectEmployees.employees.filter((e) => {
          return this.service.employees.map((em) => em.id).indexOf(e.id) !== -1;
        });
        this.selectEmployees.setValue(selectedEmployees);
        break;
    }
  }

  initForm(): void {
    this.basicDataForm = this.formBuilder.group({
      name: [ '', [ Validators.required ] ],
      serviceType: [ '', [ Validators.required ] ],
      cost: [ '', [ Validators.required ] ]
    });
  }

  updateBasicData(): void {
    if (this.basicDataForm.valid) {
      this.service.name = this.controls['name'].value;
      this.service.serviceTypeId = +this.controls['serviceType'].value;
      this.service.cost = +this.controls['cost'].value;

      this.updateService();
    }
  }

  updateEquipments(): void {
    if (this.selectEquipments.valid) {
      this.service.equipmentCodes = this.selectEquipments.value;

      this.updateService();
    }
  }

  updateProducts(): void {
    if (this.selectProducts.valid) {
      this.service.productCodes = this.selectProducts.value;

      this.updateService();
    }
  }

  updateEmployees(): void {
    if (this.selectEmployees.valid) {
      this.service.employeeCodes = this.selectEmployees.value;

      this.updateService();
    }
  }

  private updateService(): void {
    this.updatingService = true;
    this.serviceService.updateService(this.service).subscribe((serviceUpdated) => {
      if (serviceUpdated) {
        this.notificationsService.showSuccessMessage(
          `Datos del servicio ${this.service.name} actualizados con éxito.`,
          () => {
            this.service = serviceUpdated;
            this.updatingService = false;
          }
        );
      }
    });
  }
}
