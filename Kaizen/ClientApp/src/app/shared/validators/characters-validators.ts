import { AbstractControl } from '@angular/forms';

export class CharactersValidators {
	public static alphabeticCharacters(control: AbstractControl): { [key: string]: boolean } | null {
		const regex = new RegExp('^([aA-zZñÑáéíóúÁÉÍÓÚüÜ]*)$');
		const isAlphabetic = regex.test(control.value);
		if (!isAlphabetic) {
			return { notAlphabetic: true };
		} else {
			return null;
		}
	}

	public static numericCharacters(control: AbstractControl): { [key: string]: boolean } | null {
		const regex = new RegExp('^([0-9]*)$');
		const isNumeric = regex.test(control.value);
		if (!isNumeric) {
			return { notNumeric: true };
		} else {
			return null;
		}
	}
}
