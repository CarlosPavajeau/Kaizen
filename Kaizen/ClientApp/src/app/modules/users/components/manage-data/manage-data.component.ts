import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthenticationService } from '@core/authentication/authentication.service';
import { IForm } from '@core/models/form';
import { User } from '@core/models/user';
import { CLIENT_ROLE } from '@global/roles';
import { Client } from '@modules/clients/models/client';
import { ClientService } from '@modules/clients/services/client.service';
import { Employee } from '@modules/employees/models/employee';
import { EmployeeService } from '@modules/employees/services/employee.service';
import { ChangePasswordModel } from '@modules/users/models/change-password';
import { UserService } from '@modules/users/services/user.service';
import { Person } from '@shared/models/person';
import { HttpErrorHandlerService } from '@shared/services/http-error-handler.service';
import { NotificationsService } from '@shared/services/notifications.service';
import { alphabeticCharacters, numericCharacters } from '@shared/validators/characters-validators';
import { of } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Component({
  selector: 'app-manage-data',
  templateUrl: './manage-data.component.html',
  styleUrls: [ './manage-data.component.css' ]
})
export class ManageDataComponent implements OnInit, IForm {
  changePasswordForm: FormGroup;
  personalDataForm: FormGroup;

  ubicationForm: FormGroup;
  contactPeopleForm: FormGroup;

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

  get contact_people_controls(): { [key: string]: AbstractControl } {
    return this.contactPeopleForm.controls;
  }

  get ubication_controls(): { [key: string]: AbstractControl } {
    return this.ubicationForm.controls;
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

    if (this.currentUserType === CLIENT_ROLE) {
      const client = this.currentPerson as Client;
      this.contactPeopleForm.setValue({
        person_name_1: client.contactPeople[0].name,
        person_phonenumber_1: client.contactPeople[0].phonenumber,
        person_name_2: client.contactPeople[1].name,
        person_phonenumber_2: client.contactPeople[1].phonenumber
      });

      this.ubicationForm.setValue({
        city: client.clientAddress.city,
        neighborhood: client.clientAddress.neighborhood,
        street: client.clientAddress.street
      });
    }
  }

  initForm(): void {
    this.changePasswordForm = this.formBuilder.group({
      oldPassword: [ '', [ Validators.required, Validators.minLength(8), Validators.maxLength(30) ] ],
      newPassword: [ '', [ Validators.required, Validators.minLength(8), Validators.maxLength(30) ] ]
    });

    this.personalDataForm = this.formBuilder.group({
      firstName: [
        '',
        [ Validators.required, Validators.minLength(2), Validators.maxLength(20), alphabeticCharacters() ]
      ],
      secondName: [ '', [ Validators.minLength(2), Validators.maxLength(20), alphabeticCharacters() ] ],
      lastName: [
        '',
        [ Validators.required, Validators.minLength(2), Validators.maxLength(20), alphabeticCharacters() ]
      ],
      secondLastname: [ '', [ Validators.minLength(2), Validators.maxLength(20), alphabeticCharacters() ] ]
    });

    this.contactPeopleForm = this.formBuilder.group({
      person_name_1: [ '', [ Validators.required, Validators.maxLength(50) ] ],
      person_phonenumber_1: [
        '',
        [ Validators.required, Validators.minLength(10), Validators.maxLength(10), numericCharacters() ]
      ],
      person_name_2: [ '', [ Validators.required, Validators.maxLength(50) ] ],
      person_phonenumber_2: [
        '',
        [ Validators.required, Validators.minLength(10), Validators.maxLength(10), numericCharacters() ]
      ]
    });

    this.ubicationForm = this.formBuilder.group({
      city: [ '', [ Validators.required, Validators.minLength(3), Validators.maxLength(40) ] ],
      neighborhood: [ '', [ Validators.required, Validators.minLength(3), Validators.maxLength(40) ] ],
      street: [ '', [ Validators.required, Validators.minLength(3), Validators.maxLength(40) ] ]
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

      if (userRole === CLIENT_ROLE) {
        const client = {
          ...this.currentPerson,
          firstName: this.personalDataControls['firstName'].value,
          secondName: this.personalDataControls['secondName'].value,
          lastName: this.personalDataControls['lastName'].value,
          secondLastName: this.personalDataControls['secondLastname'].value
        } as Client;

        this.updateClient(client, 'Datos personales actualizados con éxito.');
      } else {
        const employee = {
          ...this.currentPerson,
          firstName: this.personalDataControls['firstName'].value,
          secondName: this.personalDataControls['secondName'].value,
          lastName: this.personalDataControls['lastName'].value,
          secondLastName: this.personalDataControls['secondLastname'].value
        } as Employee;

        this.updatingData = true;
        this.employeeService.updateEmployee(employee).subscribe((employeeUpdated) => {
          this.notificationsService.showSuccessMessage(`Datos personales actualizados con éxito.`, () => {
            this.afterUpdateData(employeeUpdated);
          });
        });
      }
    }
  }

  updateUbicationData(): void {
    if (this.ubicationForm.valid) {
      const client = {
        ...this.currentPerson,
        clientAddress: {
          city: this.ubication_controls['city'].value,
          neighborhood: this.ubication_controls['neighborhood'].value,
          street: this.ubication_controls['street'].value
        }
      } as Client;

      this.updateClient(client, 'Datos de ubicación actualizados con éxito.');
    }
  }

  updateContactPeople(): void {
    if (this.contactPeopleForm.valid) {
      const client = {
        ...this.currentPerson,
        contactPeople: [
          {
            name: this.contact_people_controls['person_name_1'].value,
            phonenumber: this.contact_people_controls['person_phonenumber_1'].value
          },
          {
            name: this.contact_people_controls['person_name_2'].value,
            phonenumber: this.contact_people_controls['person_phonenumber_2'].value
          }
        ]
      } as Client;

      this.updateClient(client, 'Personas de contacto actualizadas con éxito.');
    }
  }

  private updateClient(client: Client, successMessage: string): void {
    this.updatingData = true;
    this.clientService.updateClient(client).subscribe((clientUpdated) => {
      if (clientUpdated) {
        this.notificationsService.showSuccessMessage(successMessage, () => {
          this.afterUpdateData(clientUpdated);
        });
      }
    });
  }

  private afterUpdateData(personUpdated: Person): void {
    this.currentPerson = personUpdated;
    localStorage.setItem('current_person', JSON.stringify(this.currentPerson));
    this.updatingData = false;
  }
}
