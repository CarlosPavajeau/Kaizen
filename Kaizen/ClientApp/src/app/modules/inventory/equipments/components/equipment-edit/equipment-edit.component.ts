import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { IForm } from '@core/models/form';
import { Equipment } from '@modules/inventory/equipments/models/equipment';
import { EquipmentService } from '@modules/inventory/equipments/services/equipment.service';
import { NotificationsService } from '@shared/services/notifications.service';

@Component({
  selector: 'app-equipment-edit',
  templateUrl: './equipment-edit.component.html',
  styleUrls: [ './equipment-edit.component.css' ]
})
export class EquipmentEditComponent implements OnInit, IForm {
  equipment: Equipment;

  equipmentBasicDataForm: FormGroup;
  equipmentInInventoryForm: FormGroup;

  updatingEquipment = false;

  get controls(): { [key: string]: AbstractControl } {
    return this.equipmentBasicDataForm.controls;
  }

  get equipmentInInventoryControls(): { [key: string]: AbstractControl } {
    return this.equipmentInInventoryForm.controls;
  }

  constructor(
    private equipmentService: EquipmentService,
    private formBuilder: FormBuilder,
    private activatedRoute: ActivatedRoute,
    private notificationsService: NotificationsService
  ) {}

  ngOnInit(): void {
    this.initForm();
    this.loadData();
  }

  private loadData(): void {
    const code = this.activatedRoute.snapshot.paramMap.get('code');
    this.equipmentService.getEquipment(code).subscribe((equipment) => {
      this.equipment = equipment;
      this.afterLoadEquipment();
    });
  }

  private afterLoadEquipment(): void {
    this.equipmentBasicDataForm.setValue({
      name: this.equipment.name,
      maintenanceDate: this.equipment.maintenanceDate,
      description: this.equipment.description
    });

    this.equipmentInInventoryForm.setValue({
      amount: this.equipment.amount,
      price: this.equipment.price
    });
  }

  initForm(): void {
    this.initEquipmentBasicDataForm();
    this.initEquipmentInInventoryForm();
  }

  private initEquipmentBasicDataForm(): void {
    this.equipmentBasicDataForm = this.formBuilder.group({
      name: [ '', [ Validators.required, Validators.minLength(5), Validators.maxLength(50) ] ],
      maintenanceDate: [ '', [ Validators.required ] ],
      description: [ '', [ Validators.required ] ]
    });
  }

  private initEquipmentInInventoryForm(): void {
    this.equipmentInInventoryForm = this.formBuilder.group({
      amount: [ '', [ Validators.required ] ],
      price: [ '', [ Validators.required ] ]
    });
  }

  updateEquipmentBasicData(): void {
    if (this.equipmentBasicDataForm.valid) {
      this.equipment.name = this.controls['name'].value;
      this.equipment.maintenanceDate = this.controls['maintenanceDate'].value;
      this.equipment.description = this.controls['description'].value;

      this.updateEquipment();
    }
  }

  private updateEquipment(): void {
    this.updatingEquipment = true;
    this.equipmentService.updateEquipment(this.equipment).subscribe((upgradedEquipment) => {
      if (upgradedEquipment) {
        this.notificationsService.showSuccessMessage(
          `Se actualizaron correctamente los datos del equipo identificado con el código ${this.equipment.code}.`,
          () => {
            this.equipment = upgradedEquipment;
            this.updatingEquipment = false;
          }
        );
      }
    });
  }

  updateEquipmentInInventory(): void {
    if (this.equipmentInInventoryForm.valid) {
      this.equipment.amount = this.equipmentInInventoryControls['amount'].value;
      this.equipment.price = +this.equipmentInInventoryControls['price'].value;

      this.updateEquipment();
    }
  }
}
