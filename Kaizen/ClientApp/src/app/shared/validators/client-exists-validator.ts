import { Injectable } from '@angular/core';
import { AbstractControl, AsyncValidator, ValidationErrors } from '@angular/forms';
import { CheckClientExistsService } from '@core/services/check-client-exists.service';
import { UniqueClientDirective } from '@shared/directives/unique-client.directive';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ClientExistsValidator implements AsyncValidator {
  constructor(private checkClientService: CheckClientExistsService) {}

  validate(control: AbstractControl): Promise<ValidationErrors> | Observable<ValidationErrors> {
    const uniqueClientDirective = new UniqueClientDirective(this.checkClientService);
    return uniqueClientDirective.validate(control);
  }
}
