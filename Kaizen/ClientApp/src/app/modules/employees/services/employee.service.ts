import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Endpoints } from '@global/endpoints';
import { Employee } from '@modules/employees/models/employee';
import { EmployeeCharge } from '@modules/employees/models/employee-charge';

@Injectable({
	providedIn: 'root'
})
export class EmployeeService {
	constructor(private http: HttpClient) {}

	getEmployees(): Observable<Employee[]> {
		return this.http.get<Employee[]>(Endpoints.EmployeesUrl);
	}

	getEmployeeCharges(): Observable<EmployeeCharge[]> {
		return this.http.get<EmployeeCharge[]>(`${Endpoints.EmployeesUrl}/EmployeeCharges`);
	}

	getEmployee(id: string): Observable<Employee> {
		return this.http.get<Employee>(`${Endpoints.EmployeesUrl}/${id}`);
	}

	saveEmployee(employee: Employee): Observable<Employee> {
		return this.http.post<Employee>(Endpoints.EmployeesUrl, employee);
	}

	updateEmployee(employee: Employee): Observable<Employee> {
		return this.http.put<Employee>(`${Endpoints.EmployeesUrl}/${employee.id}`, employee);
	}
}
