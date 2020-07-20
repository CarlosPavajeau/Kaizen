import { AuthenticationService } from '@core/authentication/authentication.service';
import { CLIENT_ROLE } from '@global/roles';
import { ClientService } from '@modules/clients/services/client.service';
import { Component, OnInit } from '@angular/core';
import { EmployeeService } from '@modules/employees/services/employee.service';
import { Person } from '@shared/models/person';

@Component({
	selector: 'app-user-profile',
	templateUrl: './user-profile.component.html',
	styleUrls: [ './user-profile.component.css' ]
})
export class UserProfileComponent implements OnInit {
	person: Person;

	constructor(
		private authService: AuthenticationService,
		private clientService: ClientService,
		private employeeService: EmployeeService
	) {}

	ngOnInit(): void {
		this.loadData();
	}

	private loadData(): void {
		this.person = JSON.parse(localStorage.getItem('current_person'));

		if (this.person == null) {
			const userRole = this.authService.getUserRole();
			const user_id = this.authService.getCurrentUser().id;
			if (userRole === CLIENT_ROLE) {
				this.clientService.getClient(user_id).subscribe((client) => {
					this.person = client;
					this.savePersonInLocalStorage();
				});
			} else {
				this.employeeService.getEmployee(user_id).subscribe((employee) => {
					this.person = employee;
					this.savePersonInLocalStorage();
				});
			}
		}
	}

	private savePersonInLocalStorage(): void {
		localStorage.setItem('current_person', JSON.stringify(this.person));
	}
}
