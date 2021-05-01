import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { ServiceType } from '@modules/services/models/service-type';

@Component({
  selector: 'app-service-type-register',
  templateUrl: './service-type-register.component.html'
})
export class ServiceTypeRegisterComponent implements OnInit {
  serviceTypeForm: FormGroup;

  constructor(public dialogRef: MatDialogRef<ServiceTypeRegisterComponent>, private formBuilder: FormBuilder) {
  }

  ngOnInit(): void {
    this.serviceTypeForm = this.formBuilder.group({
      name: [ '', [ Validators.required, Validators.minLength(5), Validators.maxLength(70) ] ],
    });
  }

  onCancel(): void {
    this.dialogRef.close();
  }

  saveServiceType(): void {
    if (this.serviceTypeForm.valid) {
      const serviceType: ServiceType = {
        ...this.serviceTypeForm.value
      };

      this.dialogRef.close(serviceType);
    }
  }

}
