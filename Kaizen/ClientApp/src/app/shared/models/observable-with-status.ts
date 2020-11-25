export enum ObservableStatus {
  Success = 'Success',
  Error = 'Error',
  Loading = 'Loading'
}

export interface ObservableWithStatus<T> {
  status: ObservableStatus;
  value?: T;
  error: Error;
}
