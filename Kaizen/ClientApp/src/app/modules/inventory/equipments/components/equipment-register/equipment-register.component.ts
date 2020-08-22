import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { IForm } from '@core/models/form';
import { Equipment } from '@modules/inventory/equipments/models/equipment';
import { EquipmentService } from '@modules/inventory/equipments/services/equipment.service';
import { NotificationsService } from '@shared/services/notifications.service';
import { EquipmentExistsValidator } from '@shared/validators/equipment-exists-validator';

@Component({
  selector: 'app-equipment-register',
  templateUrl: './equipment-register.component.html',
  styleUrls: [ './equipment-register.component.css' ]
})
export class EquipmentRegisterComponent implements OnInit, IForm {
  equipmentForm: FormGroup;
  equipmentBuyForm: FormGroup;

  savingData = false;

  public get controls(): { [key: string]: AbstractControl } {
    return this.equipmentForm.controls;
  }

  public get buy_controls(): { [key: string]: AbstractControl } {
    return this.equipmentBuyForm.controls;
  }

  constructor(
    private equipmentService: EquipmentService,
    private formBuilder: FormBuilder,
    private equipmentValidator: EquipmentExistsValidator,
    private notificationService: NotificationsService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.initForm();
    this.initEquipmentBuyForm();
  }

  initForm(): void {
    this.equipmentForm = this.formBuilder.group({
      code: [
        '',
        {
          validators: [ Validators.required, Validators.minLength(3), Validators.maxLength(20) ],
          asyncValidators: [ this.equipmentValidator.validate.bind(this.equipmentValidator) ],
          updateOn: 'blur'
        }
      ],
      name: [ '', [ Validators.required, Validators.minLength(5), Validators.maxLength(50) ] ],
      maintenanceDate: [ new Date(), [ Validators.required ] ]
    });
  }

  private initEquipmentBuyForm(): void {
    this.equipmentBuyForm = this.formBuilder.group({
      amount: [ '', [ Validators.required ] ],
      price: [ '', [ Validators.required ] ],
      description: [ '', [ Validators.required ] ]
    });
  }

  onSubmit() {
    if (this.equipmentForm.valid && this.equipmentBuyForm.valid) {
      const equipment: Equipment = this.mapEquipment();

      this.savingData = true;
      this.equipmentService.saveEquipment(equipment).subscribe((equipmentSave) => {
        this.notificationService.showSuccessMessage(
          `El equipo ${equipmentSave.name} ha sido registrado con éxito`,
          () => {
            this.router.navigateByUrl('/inventory/equipments');
          }
        );
      });
    }
  }

  private mapEquipment(): Equipment {
    return {
      code: this.controls['code'].value,
      name: this.controls['name'].value,
      maintenanceDate: this.controls['maintenanceDate'].value,
      amount: this.buy_controls['amount'].value,
      price: this.buy_controls['price'].value,
      description: this.buy_controls['description'].value
    };
  }
}
