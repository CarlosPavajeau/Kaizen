import { Component, OnInit, ViewChild } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatTabChangeEvent } from '@angular/material/tabs';
import { ActivatedRoute, Router } from '@angular/router';
import { IForm } from '@core/models/form';
import { SelectEmployeesComponent } from '@modules/services/components/select-employees/select-employees.component';
import { SelectEquipmentsComponent } from '@modules/services/components/select-equipments/select-equipments.component';
import { SelectProductsComponent } from '@modules/services/components/select-products/select-products.component';
import { Service } from '@modules/services/models/service';
import { ServiceType } from '@modules/services/models/service-type';
import { ServiceService } from '@modules/services/services/service.service';
import { ObservableStatus } from '@shared/models/observable-with-status';
import { DialogsService } from '@shared/services/dialogs.service';
import { NotificationsService } from '@shared/services/notifications.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-service-edit',
  templateUrl: './service-edit.component.html',
  styleUrls: [ './service-edit.component.scss' ]
})
export class ServiceEditComponent implements OnInit, IForm {
  public ObsStatus: typeof ObservableStatus = ObservableStatus;

  service$: Observable<Service>;
  serviceTypes$: Observable<ServiceType[]>;

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
    private dialogsService: DialogsService,
    private router: Router
  ) {
  }

  ngOnInit(): void {
    this.initForm();
    this.loadData();
  }

  private loadData(): void {
    this.serviceTypes$ = this.serviceService.getServiceTypes();

    const code = this.activatedRoute.snapshot.paramMap.get('code');
    this.service$ = this.serviceService.getService(code);
    this.service$.subscribe((service) => {
      this.afterLoadService(service);
    });
  }

  private afterLoadService(service: Service): void {
    this.basicDataForm.setValue({
      name: service.name,
      serviceType: service.serviceTypeId,
      cost: service.cost
    });
  }

  selectedTabChange(event: MatTabChangeEvent, service: Service): void {
    switch (event.index) {
      case 1:
        this.selectEquipments.setValue(service.equipments);
        break;
      case 2:
        this.selectProducts.setValue(service.products);
        break;
      case 3:
        this.selectEmployees.setValue(service.employees);
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

  updateBasicData(service: Service): void {
    if (this.basicDataForm.valid) {
      service.name = this.controls['name'].value;
      service.serviceTypeId = +this.controls['serviceType'].value;
      service.cost = +this.controls['cost'].value;

      this.updateService(service);
    }
  }

  updateEquipments(service: Service): void {
    if (this.selectEquipments.valid) {
      service.equipmentCodes = this.selectEquipments.value;

      this.updateService(service);
    }
  }

  updateProducts(service: Service): void {
    if (this.selectProducts.valid) {
      service.productCodes = this.selectProducts.value;

      this.updateService(service);
    }
  }

  updateEmployees(service: Service): void {
    if (this.selectEmployees.valid) {
      service.employeeCodes = this.selectEmployees.value;

      this.updateService(service);
    }
  }

  private updateService(service: Service): void {
    this.updatingService = true;
    this.serviceService.updateService(service).subscribe((serviceUpdated) => {
      if (serviceUpdated) {
        this.dialogsService.showSuccessDialog(
          `Datos del servicio ${ service.name } actualizados con éxito.`,
          () => {
            this.updatingService = false;
            this.router.navigateByUrl('/services');
          }
        );
      }
    });
  }
}
