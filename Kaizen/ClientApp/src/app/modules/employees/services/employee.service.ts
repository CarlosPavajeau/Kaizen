import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Employee } from '../models/employee';
import { Endpoints } from '@app/global/endpoints';

@Injectable({
	providedIn: 'root'
})
export class EmployeeService {
	constructor(private http: HttpClient) {}

	getEmployees(): Observable<Employee[]> {
		return this.http.get<Employee[]>(Endpoints.EmployeesUrl);
	}

	getEmployee(id: string): Observable<Employee> {
		return this.http.get<Employee>(`${Endpoints.EmployeesUrl}/${id}`);
	}

	saveEmployee(employee: Employee): Observable<Employee> {
		return this.http.post<Employee>(Endpoints.EmployeesUrl, employee);
	}

	updateEmployee(employee: Employee): Observable<Employee> {
		return this.http.put<Employee>(Endpoints.EmployeesUrl, employee);
	}
}
