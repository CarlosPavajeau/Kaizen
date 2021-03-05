import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { EmployeeCharge } from '@modules/employees/models/employee-charge';

@Component({
  selector: 'app-employee-charge-register',
  templateUrl: './employee-charge-register.component.html',
  styleUrls: [ './employee-charge-register.component.scss' ]
})
export class EmployeeChargeRegisterComponent implements OnInit {
  employeeChargeForm: FormGroup;

  constructor(public dialogRef: MatDialogRef<EmployeeChargeRegisterComponent>, private formBuilder: FormBuilder) {
  }

  ngOnInit(): void {
    this.employeeChargeForm = this.formBuilder.group({
      charge: [ '', [ Validators.required, Validators.minLength(5), Validators.maxLength(50) ] ]
    })
  }

  onCancel(): void {
    this.dialogRef.close();
  }

  saveEmployeeCharge(): void {
    if (this.employeeChargeForm.valid) {
      const employeeCharge: EmployeeCharge = {
        ...this.employeeChargeForm.value
      };

      this.dialogRef.close(employeeCharge);
    }
  }
}
