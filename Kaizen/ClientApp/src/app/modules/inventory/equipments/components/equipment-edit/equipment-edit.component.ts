import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { IForm } from '@core/models/form';
import { Equipment } from '@modules/inventory/equipments/models/equipment';
import { EquipmentService } from '@modules/inventory/equipments/services/equipment.service';
import { ObservableStatus } from '@shared/models/observable-with-status';
import { DialogsService } from '@shared/services/dialogs.service';
import { NotificationsService } from '@shared/services/notifications.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-equipment-edit',
  templateUrl: './equipment-edit.component.html'
})
export class EquipmentEditComponent implements OnInit, IForm {
  public ObsStatus: typeof ObservableStatus = ObservableStatus;

  equipment$: Observable<Equipment>;

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
    private dialogsService: DialogsService,
    private router: Router
  ) {
  }

  ngOnInit(): void {
    this.initForm();
    this.loadData();
  }

  private loadData(): void {
    const code = this.activatedRoute.snapshot.paramMap.get('code');
    this.equipment$ = this.equipmentService.getEquipment(code);
    this.equipment$.subscribe((equipment) => {
      this.afterLoadEquipment(equipment);
    });
  }

  private afterLoadEquipment(equipment: Equipment): void {
    this.equipmentBasicDataForm.setValue({
      name: equipment.name,
      maintenanceDate: equipment.maintenanceDate,
      description: equipment.description
    });

    this.equipmentInInventoryForm.setValue({
      amount: equipment.amount,
      price: equipment.price
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

  updateEquipmentBasicData(equipment: Equipment): void {
    if (this.equipmentBasicDataForm.valid) {
      equipment.name = this.controls['name'].value;
      equipment.maintenanceDate = this.controls['maintenanceDate'].value;
      equipment.description = this.controls['description'].value;

      this.updateEquipment(equipment);
    }
  }

  private updateEquipment(equipment: Equipment): void {
    this.updatingEquipment = true;
    this.equipmentService.updateEquipment(equipment).subscribe((upgradedEquipment) => {
      if (upgradedEquipment) {
        this.dialogsService.showSuccessDialog(
          `Se actualizaron correctamente los datos del equipo identificado con el código ${ equipment.code }.`,
          () => {
            this.router.navigateByUrl('/equipments');
            this.updatingEquipment = false;
          }
        );
      }
    });
  }

  updateEquipmentInInventory(equipment: Equipment): void {
    if (this.equipmentInInventoryForm.valid) {
      equipment.amount = this.equipmentInInventoryControls['amount'].value;
      equipment.price = +this.equipmentInInventoryControls['price'].value;

      this.updateEquipment(equipment);
    }
  }
}
