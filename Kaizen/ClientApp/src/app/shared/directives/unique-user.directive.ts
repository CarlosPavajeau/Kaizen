import { Directive } from '@angular/core';
import { NG_ASYNC_VALIDATORS, AsyncValidator } from '@angular/forms';
import { CheckUserExistsService } from 'src/app/core/services/check-user-exists.service';
import { map } from 'rxjs/operators';

@Directive({
  selector: '[uniqueUser]',
  providers: [{provide: NG_ASYNC_VALIDATORS, useExisting: UniqueUserDirective, multi: true}]
})
export class UniqueUserDirective implements AsyncValidator {

  constructor(
    private checkUserService: CheckUserExistsService
  ) { }

  validate(control: import("@angular/forms").AbstractControl): Promise<import("@angular/forms").ValidationErrors> | import("rxjs").Observable<import("@angular/forms").ValidationErrors> {
    const username = control.value;
    return this.checkUserService.checkUserExists(username)
      .pipe(
        map(result => {
          if (result) {
            return { userExists: true };
          }
          return null;
        })
      );
  }
}
