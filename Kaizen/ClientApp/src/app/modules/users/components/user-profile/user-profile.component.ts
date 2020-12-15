import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '@core/authentication/authentication.service';
import { ADMINISTRATOR_ROLE, CLIENT_ROLE } from '@global/roles';
import { ClientService } from '@modules/clients/services/client.service';
import { EmployeeService } from '@modules/employees/services/employee.service';
import { Person } from '@shared/models/person';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: [ './user-profile.component.scss' ]
})
export class UserProfileComponent implements OnInit {
  person: Person;
  isAdmin = false;

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
    const userRole = this.authService.getUserRole();
    this.isAdmin = userRole === ADMINISTRATOR_ROLE;
    if (this.person == null) {
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
