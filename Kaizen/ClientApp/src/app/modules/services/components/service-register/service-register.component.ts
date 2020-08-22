import { Component, OnInit, ViewChild } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { IForm } from '@core/models/form';
import { SelectEmployeesComponent } from '@modules/services/components/select-employees/select-employees.component';
import { SelectEquipmentsComponent } from '@modules/services/components/select-equipments/select-equipments.component';
import { SelectProductsComponent } from '@modules/services/components/select-products/select-products.component';
import { Service } from '@modules/services/models/service';
import { ServiceType } from '@modules/services/models/service-type';
import { ServiceService } from '@modules/services/services/service.service';
import { NotificationsService } from '@shared/services/notifications.service';

@Component({
  selector: 'app-service-register',
  templateUrl: './service-register.component.html',
  styleUrls: [ './service-register.component.css' ]
})
export class ServiceRegisterComponent implements OnInit, IForm {
  serviceTypes: ServiceType[] = [];

  serviceForm: FormGroup;

  @ViewChild('selectProducts') selectProducts: SelectProductsComponent;
  @ViewChild('selectEmployees') selectEmployees: SelectEmployeesComponent;
  @ViewChild('selectEquipments') selectEquipments: SelectEquipmentsComponent;

  savingData = false;

  public get controls(): { [key: string]: AbstractControl } {
    return this.serviceForm.controls;
  }

  constructor(
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
  }

  onSubmit(): void {
    if (this.allFormsValid()) {
      const service = this.mapService();

      this.savingData = true;
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
      this.serviceForm.valid && this.selectProducts.valid && this.selectEquipments.valid && this.selectEmployees.valid
    );
  }

  private mapService(): Service {
    return {
      code: this.controls['code'].value,
      name: this.controls['name'].value,
      serviceTypeId: +this.controls['serviceType'].value,
      cost: +this.controls['cost'].value,
      productCodes: this.selectProducts.value,
      equipmentCodes: this.selectEquipments.value,
      employeeCodes: this.selectEmployees.value
    };
  }
}
