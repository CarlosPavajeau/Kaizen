import { Pipe, PipeTransform } from '@angular/core';
import { APPLICATION_MONTHS } from '@app/core/models/months';

@Pipe({
  name: 'monthBit'
})
export class MonthBitPipe implements PipeTransform {
  transform(bitMask: number): string {
    if (!bitMask) {
      return 'None';
    } else {
      let months = '';
      let monthAdded = false;
      APPLICATION_MONTHS.forEach((month) => {
        // tslint:disable-next-line: no-bitwise
        if (bitMask & month.value) {
          if (!monthAdded) {
            months += month.name;
            monthAdded = true;
          } else {
            months += `, ${month.name}`;
          }
        }
      });
      return months + '.';
    }
  }
}
