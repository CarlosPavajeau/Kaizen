import { Directive } from '@angular/core';
import { AbstractControl, AsyncValidator, NG_ASYNC_VALIDATORS, ValidationErrors } from '@angular/forms';
import { CheckUserExistsService } from '@core/services/check-user-exists.service';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Directive({
  selector: '[appUniqueUser]',
  providers: [ { provide: NG_ASYNC_VALIDATORS, useExisting: UniqueUserDirective, multi: true } ]
})
export class UniqueUserDirective implements AsyncValidator {
  constructor(private checkUserService: CheckUserExistsService) {}

  validate(control: AbstractControl): Promise<ValidationErrors> | Observable<ValidationErrors> {
    const username = control.value;
    return this.checkUserService.checkEntityExists(username).pipe(
      map((result) => {
        if (result) {
          return { userExists: true };
        }
        return null;
      })
    );
  }
}
