import { Directive } from '@angular/core';
import { NG_ASYNC_VALIDATORS, AsyncValidator, AbstractControl, ValidationErrors } from '@angular/forms';
import { Observable } from 'rxjs';
import { CheckProductExistsService } from '@core/services/check-product-exists.service';
import { map } from 'rxjs/operators';

@Directive({
  selector: '[appUniqueProduct]',
  providers: [ { provide: NG_ASYNC_VALIDATORS, useExisting: UniqueProductDirective, multi: true } ]
})
export class UniqueProductDirective implements AsyncValidator {
  constructor(private checkProductExistsService: CheckProductExistsService) {}

  validate(control: AbstractControl): Promise<ValidationErrors> | Observable<ValidationErrors> {
    const id = control.value;
    return this.checkProductExistsService.checkEntityExists(id).pipe(
      map((result) => {
        if (result) {
          return { productExists: true };
        }
        return null;
      })
    );
  }
}
