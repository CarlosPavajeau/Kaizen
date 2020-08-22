import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { User } from '@core/models/user';
import { NotificationsService } from '@shared/services/notifications.service';
import { AuthenticationService } from '@core/authentication/authentication.service';
import { IForm } from '@core/models/form';
import { ChangePasswordModel } from '@modules/users/models/change-password';
import { Person } from '@shared/models/person';
import { HttpErrorHandlerService } from '@shared/services/http-error-handler.service';
import { of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { UserService } from '@modules/users/services/user.service';
import { CharactersValidators } from '@shared/validators/characters-validators';
import { ClientService } from '@modules/clients/services/client.service';
import { EmployeeService } from '@modules/employees/services/employee.service';
import { CLIENT_ROLE } from '@app/global/roles';
import { Client } from '@app/modules/clients/models/client';
import { Employee } from '@app/modules/employees/models/employee';

@Component({
  selector: 'app-manage-data',
  templateUrl: './manage-data.component.html',
  styleUrls: [ './manage-data.component.css' ]
})
export class ManageDataComponent implements OnInit, IForm {
  changePasswordForm: FormGroup;
  personalDataForm: FormGroup;

  currentPerson: Person;
  currentUserType: string;
  hideOldPassword = true;
  hideNewPassword = true;

  updatingData = false;

  get controls(): { [key: string]: AbstractControl } {
    return this.changePasswordForm.controls;
  }

  get personalDataControls(): { [key: string]: AbstractControl } {
    return this.personalDataForm.controls;
  }

  constructor(
    private userService: UserService,
    private formBuilder: FormBuilder,
    private authService: AuthenticationService,
    private clientService: ClientService,
    private employeeService: EmployeeService,
    private notificationsService: NotificationsService,
    private errorHandlerService: HttpErrorHandlerService
  ) {}

  ngOnInit(): void {
    this.loadData();
    this.initForm();
  }

  private loadData(): void {
    this.currentUserType = this.authService.getUserRole();
    this.currentPerson = JSON.parse(localStorage.getItem('current_person'));
    if (this.currentUserType === CLIENT_ROLE) {
      this.clientService.getClient(this.currentPerson.id).subscribe((client) => {
        this.currentPerson = client;
        this.afterLoadCurrentPerson();
      });
    } else {
      this.employeeService.getEmployee(this.currentPerson.id).subscribe((employee) => {
        this.currentPerson = employee;
        this.afterLoadCurrentPerson();
      });
    }
  }

  private afterLoadCurrentPerson(): void {
    this.personalDataForm.setValue({
      firstName: this.currentPerson.firstName,
      secondName: this.currentPerson.secondName,
      lastName: this.currentPerson.lastName,
      secondLastname: this.currentPerson.secondLastName
    });
  }

  initForm(): void {
    this.changePasswordForm = this.formBuilder.group({
      oldPassword: [ '', [ Validators.required, Validators.minLength(8), Validators.maxLength(30) ] ],
      newPassword: [ '', [ Validators.required, Validators.minLength(8), Validators.maxLength(30) ] ]
    });

    this.personalDataForm = this.formBuilder.group({
      firstName: [
        '',
        [
          Validators.required,
          Validators.minLength(2),
          Validators.maxLength(20),
          CharactersValidators.alphabeticCharacters
        ]
      ],
      secondName: [
        '',
        [ Validators.minLength(2), Validators.maxLength(20), CharactersValidators.alphabeticCharacters ]
      ],
      lastName: [
        '',
        [
          Validators.required,
          Validators.minLength(2),
          Validators.maxLength(20),
          CharactersValidators.alphabeticCharacters
        ]
      ],
      secondLastname: [
        '',
        [ Validators.minLength(2), Validators.maxLength(20), CharactersValidators.alphabeticCharacters ]
      ]
    });
  }

  changePassword(): void {
    if (this.changePasswordForm.valid) {
      const changePasswordModel: ChangePasswordModel = { ...this.changePasswordForm.value };
      const currentUser: User = this.authService.getCurrentUser();

      this.updatingData = true;
      this.userService
        .changePassword(currentUser.id, changePasswordModel)
        .pipe(
          catchError((error: any) => {
            this.errorHandlerService.handleHttpError(error, () => {
              this.updatingData = false;
            });
            return of(null);
          })
        )
        .subscribe((userUpdated) => {
          if (userUpdated) {
            this.notificationsService.showSuccessMessage('Contraseña de acceso modificada con éxito.', () => {
              this.authService.logoutUser();
              location.reload();
            });
          }
        });
    }
  }

  updatePersonalData(): void {
    if (this.personalDataForm.valid) {
      const userRole = this.authService.getUserRole();
      this.updatingData = true;
      if (userRole === CLIENT_ROLE) {
        const client = {
          ...this.currentPerson,
          firstName: this.personalDataControls['firstName'].value,
          secondName: this.personalDataControls['secondName'].value,
          lastName: this.personalDataControls['lastName'].value,
          secondLastName: this.personalDataControls['secondLastname'].value
        } as Client;

        this.clientService.updateClient(client).subscribe((clientUpdated) => {
          this.notificationsService.showSuccessMessage(`Datos personales actualizados con éxito.`, () => {
            this.currentPerson = clientUpdated;
            localStorage.setItem('current_person', JSON.stringify(this.currentPerson));
            this.updatingData = false;
          });
        });
      } else {
        const employee = {
          ...this.currentPerson,
          firstName: this.personalDataControls['firstName'].value,
          secondName: this.personalDataControls['secondName'].value,
          lastName: this.personalDataControls['lastName'].value,
          secondLastName: this.personalDataControls['secondLastname'].value
        } as Employee;

        this.employeeService.updateEmployee(employee).subscribe((employeeUpdated) => {
          this.notificationsService.showSuccessMessage(`Datos personales actualizados con éxito.`, () => {
            this.currentPerson = employeeUpdated;
            localStorage.setItem('current_person', JSON.stringify(this.currentPerson));
            this.updatingData = false;
          });
        });
      }
    }
  }
}
