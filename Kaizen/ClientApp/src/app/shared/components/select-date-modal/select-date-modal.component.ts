import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormControl, Validators, FormBuilder, AbstractControl, FormGroup } from '@angular/forms';
import { zeroPad } from '@app/core/utils/number-utils';

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
			const isoDate = new Date(
				`${date.getFullYear()}-${zeroPad(date.getMonth() + 1, 2)}-${zeroPad(date.getDate(), 2)}T${time}:00Z`
			);
			this.dialogRef.close(isoDate);
		}
	}
}
