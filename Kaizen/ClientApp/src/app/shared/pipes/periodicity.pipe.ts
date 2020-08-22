import { Pipe, PipeTransform } from '@angular/core';
import { PeriodicityType } from '@modules/service-requests/models/periodicity-type';

@Pipe({
  name: 'periodicity'
})
export class PeriodicityPipe implements PipeTransform {
  transform(value: PeriodicityType): string {
    switch (value) {
      case PeriodicityType.Annual:
        return 'Anual';
      case PeriodicityType.BiMonthly:
        return 'Bi-Mensual';
      case PeriodicityType.Biannual:
        return 'Semestral';
      case PeriodicityType.Biweekly:
        return 'Quincenal';
      case PeriodicityType.Casual:
        return 'Casual';
      case PeriodicityType.Monthly:
        return 'Mensual';
      case PeriodicityType.Quarter:
        return 'Cuatrimestral';
      case PeriodicityType.Quinquemestre:
        return 'Quimensual';
      case PeriodicityType.Trimester:
        return 'Trimestral';
    }
  }
}
