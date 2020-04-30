import { AbstractControl } from '@angular/forms';

export interface IForm {
	controls: {
		[key: string]: AbstractControl;
	};

	initForm(): void;
}
