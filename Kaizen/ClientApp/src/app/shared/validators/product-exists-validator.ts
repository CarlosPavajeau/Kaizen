import { Injectable } from '@angular/core';
import { AbstractControl, AsyncValidator, ValidationErrors } from '@angular/forms';
import { CheckProductExistsService } from '@core/services/check-product-exists.service';
import { UniqueProductDirective } from '@shared/directives/unique-product.directive';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProductExistsValidator implements AsyncValidator {
  constructor(private checkProductExists: CheckProductExistsService) {}

  validate(control: AbstractControl): Promise<ValidationErrors> | Observable<ValidationErrors> {
    const uniqueProductDirective = new UniqueProductDirective(this.checkProductExists);
    return uniqueProductDirective.validate(control);
  }
}
