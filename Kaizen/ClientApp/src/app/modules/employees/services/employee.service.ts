import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { EMPLOYEES_API_URL } from '@global/endpoints';
import { Employee } from '@modules/employees/models/employee';
import { EmployeeCharge } from '@modules/employees/models/employee-charge';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
  constructor(private http: HttpClient) {}

  getEmployees(): Observable<Employee[]> {
    return this.http.get<Employee[]>(EMPLOYEES_API_URL);
  }

  getEmployeeCharges(): Observable<EmployeeCharge[]> {
    return this.http.get<EmployeeCharge[]>(`${EMPLOYEES_API_URL}/EmployeeCharges`);
  }

  getEmployee(id: string): Observable<Employee> {
    return this.http.get<Employee>(`${EMPLOYEES_API_URL}/${id}`);
  }

  getTechniciansAvailable(date: Date, serviceCodes: string[]): Observable<Employee[]> {
    return this.http.get<Employee[]>(
      `${EMPLOYEES_API_URL}/TechniciansAvailable?date=${date.toJSON()}&serviceCodes=${serviceCodes}`
    );
  }

  saveEmployee(employee: Employee): Observable<Employee> {
    return this.http.post<Employee>(EMPLOYEES_API_URL, employee);
  }

  updateEmployee(employee: Employee): Observable<Employee> {
    return this.http.put<Employee>(`${EMPLOYEES_API_URL}/${employee.id}`, employee);
  }
}
