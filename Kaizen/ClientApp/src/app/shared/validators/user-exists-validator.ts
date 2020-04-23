import { Injectable } from "@angular/core";
import { AsyncValidator, AbstractControl, ValidationErrors } from "@angular/forms";
import { CheckUserExistsService } from "src/app/core/services/check-user-exists.service";
import { UniqueUserDirective } from "../directives/unique-user.directive";
import { Observable } from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class UserExistsValidator implements AsyncValidator {

  constructor(
    private checkUserService: CheckUserExistsService
  ) { }

  validate(control: AbstractControl): Promise<ValidationErrors> | Observable<ValidationErrors> {
    const uniqueUserDirective = new UniqueUserDirective(this.checkUserService);
    return uniqueUserDirective.validate(control);
  }
}
