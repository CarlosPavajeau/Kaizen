import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormControl, Validators, FormBuilder, AbstractControl, FormGroup } from '@angular/forms';

@Component({
	selector: 'app-select-date-modal',
	templateUrl: './select-date-modal.component.html',
	styleUrls: [ './select-date-modal.component.css' ]
})
export class SelectDateModalComponent implements OnInit {
	dateForm: FormGroup;

	constructor(public dialogRef: MatDialogRef<SelectDateModalComponent>, private formBuilder: FormBuilder) {}

	ngOnInit(): void {
		this.dateForm = this.formBuilder.group({
			newDate: [ '', [ Validators.required ] ],
			newTime: [ '', [ Validators.required ] ]
		});
	}

	onCancel(): void {
		this.dialogRef.close();
	}

	saveDate(): void {
		if (this.dateForm.valid) {
			const date = this.dateForm.controls['newDate'].value as Date;
			const time = this.dateForm.controls['newTime'].value;
			const timeDate = new Date(`1/1/1970 ${time}:00`);
			date.setHours(timeDate.getHours());
			date.setMinutes(timeDate.getMinutes());
			this.dialogRef.close(date);
		}
	}
}
