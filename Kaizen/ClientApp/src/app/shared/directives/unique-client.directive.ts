import { Directive } from '@angular/core';
import { AbstractControl, AsyncValidator, NG_ASYNC_VALIDATORS, ValidationErrors } from '@angular/forms';
import { CheckClientExistsService } from '@core/services/check-client-exists.service';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Directive({
  selector: '[appUniqueClient]',
  providers: [ { provide: NG_ASYNC_VALIDATORS, useExisting: UniqueClientDirective, multi: true } ]
})
export class UniqueClientDirective implements AsyncValidator {
  constructor(private checkClientService: CheckClientExistsService) {}

  validate(control: AbstractControl): Promise<ValidationErrors> | Observable<ValidationErrors> {
    const id = control.value;
    return this.checkClientService.checkEntityExists(id).pipe(
      map((result) => {
        if (result) {
          return { clientExists: true };
        }
        return null;
      })
    );
  }
}
