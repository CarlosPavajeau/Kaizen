import { Pipe, PipeTransform } from '@angular/core';
import { ApplicationMonths, APPLICATION_MONTHS } from '@core/models/months';

@Pipe({
  name: 'applicationMonths'
})
export class ApplicationMonthsPipe implements PipeTransform {
  transform(value: ApplicationMonths): unknown {
    if (value === ApplicationMonths.None) {
      return 'None';
    }

    return (
      APPLICATION_MONTHS.filter((month) => {
        // tslint:disable-next-line: no-bitwise
        return month.value & value;
      })
        .map((m) => m.name)
        .join(', ') + '.'
    );
  }
}
