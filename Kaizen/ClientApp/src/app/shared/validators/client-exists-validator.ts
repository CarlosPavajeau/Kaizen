import { Observable } from "rxjs";
import { AbstractControl, ValidationErrors, AsyncValidator } from "@angular/forms";
import { CheckClientExistsService } from "src/app/core/services/check-client-exists.service";
import { UniqueClientDirective } from "../directives/unique-client.directive";
import { Injectable } from "@angular/core";

@Injectable({
  providedIn: 'root'
})
export class ClientExistsValidator implements AsyncValidator {

  constructor(
    private checkClientService: CheckClientExistsService
  ) { }

  validate(control: AbstractControl): Promise<ValidationErrors> | Observable<ValidationErrors> {
    const uniqueClientDirective = new UniqueClientDirective(this.checkClientService);
    return uniqueClientDirective.validate(control);
  }
}
