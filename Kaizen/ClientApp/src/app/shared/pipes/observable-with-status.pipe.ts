import { Pipe, PipeTransform } from '@angular/core';
import { ObservableStatus, ObservableWithStatus } from '@shared/models/observable-with-status';
import { Observable, of } from 'rxjs';
import { catchError, map, startWith } from 'rxjs/operators';

@Pipe({
  name: 'observableWithStatus'
})
export class ObservableWithStatusPipe implements PipeTransform {
  transform<T = any>(value: Observable<T>): Observable<ObservableWithStatus<T>> {
    return value.pipe(
      map((val: any) => {
        return {
          status: ObservableStatus.Success,
          value: val,
          error: null
        };
      }),
      startWith({ status: ObservableStatus.Loading, value: null, error: null }),
      catchError((err: Error) => {
        return of({ status: ObservableStatus.Error, value: null, error: err });
      })
    );
  }
}
