import { Employee } from './employee';

export interface Location {
  latitude: number;
  longitude: number;
}

export interface EmployeeLocation {
  id: string;

  employee: Employee;

  location: Location;
}
