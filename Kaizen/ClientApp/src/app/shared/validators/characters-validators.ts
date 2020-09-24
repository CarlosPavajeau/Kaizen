import { AbstractControl, ValidatorFn } from '@angular/forms';

export function alphabeticCharacters(): ValidatorFn {
  return (control: AbstractControl): { [key: string]: any } | null => {
    const regex = new RegExp('^([aA-zZñÑáéíóúÁÉÍÓÚüÜ]*)$');
    const isAlphabetic = regex.test(control.value);

    return isAlphabetic ? null : { notAlphabetic: true };
  };
}

export function numericCharacters(): ValidatorFn {
  return (control: AbstractControl): { [key: string]: any } | null => {
    const regex = new RegExp('^([0-9]*)$');
    const isNumeric = regex.test(control.value);

    return isNumeric ? null : { notNumeric: true };
  };
}
