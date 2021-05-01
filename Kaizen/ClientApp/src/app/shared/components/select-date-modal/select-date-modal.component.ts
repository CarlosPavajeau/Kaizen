import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { buildIsoDate } from '@core/utils/date-utils';

@Component({
  selector: 'app-select-date-modal',
  templateUrl: './select-date-modal.component.html'
})
export class SelectDateModalComponent implements OnInit {
  dateForm: FormGroup;

  minDate: Date;
  maxDate: Date;

  constructor(public dialogRef: MatDialogRef<SelectDateModalComponent>, private formBuilder: FormBuilder) {
  }

  ngOnInit(): void {
    this.dateForm = this.formBuilder.group({
      newDate: [ '', [ Validators.required ] ],
      newTime: [ '', [ Validators.required ] ]
    });

    const currentDate = new Date();
    this.minDate = new Date(currentDate.getFullYear(), currentDate.getMonth(), currentDate.getDate() + 1);
    this.maxDate = new Date(currentDate.getFullYear(), 11, 31);
  }

  onCancel(): void {
    this.dialogRef.close();
  }

  saveDate(): void {
    if (this.dateForm.valid) {
      const date = this.dateForm.controls['newDate'].value as Date;
      const time = this.dateForm.controls['newTime'].value;
      const isoDate = buildIsoDate(date, time);
      this.dialogRef.close(isoDate);
    }
  }
}
