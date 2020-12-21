import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthenticationService } from '@core/authentication/authentication.service';
import { IForm } from '@core/models/form';
import { User } from '@core/models/user';
import { Client } from '@modules/clients/models/client';
import { ClientState } from '@modules/clients/models/client-state';
import { ClientService } from '@modules/clients/services/client.service';
import { NotificationsService } from '@shared/services/notifications.service';
import { alphabeticCharacters, numericCharacters } from '@shared/validators/characters-validators';

@Component({
  selector: 'app-client-register',
  templateUrl: './client-register.component.html',
  styleUrls: [ './client-register.component.scss' ]
})
export class ClientRegisterComponent implements OnInit, IForm {
  clientForm: FormGroup;
  legalPersonForm: FormGroup;
  contactPersonForm: FormGroup;
  contactPeopleForm: FormGroup;
  ubicationForm: FormGroup;
  savingData = false;

  public get controls(): { [key: string]: AbstractControl } {
    return this.clientForm.controls;
  }

  public get legal_controls(): { [key: string]: AbstractControl } {
    return this.legalPersonForm.controls;
  }

  public get contact_controls(): { [key: string]: AbstractControl } {
    return this.contactPersonForm.controls;
  }

  public get ubication_controls(): { [key: string]: AbstractControl } {
    return this.ubicationForm.controls;
  }

  public get contact_people_controls(): { [key: string]: AbstractControl } {
    return this.contactPeopleForm.controls;
  }

  constructor(
    private clientService: ClientService,
    private authService: AuthenticationService,
    private formBuilder: FormBuilder,
    private notificationsService: NotificationsService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.initForm();
  }

  initForm() {
    this.initClientForm();
    this.initLegalPersonForm();
    this.initContactForm();
    this.initContactPeopleForm();
    this.initUbicationForm();
  }

  private initUbicationForm() {
    this.ubicationForm = this.formBuilder.group({
      city: [ '', [ Validators.required, Validators.minLength(3), Validators.maxLength(40) ] ],
      neighborhood: [ '', [ Validators.required, Validators.minLength(3), Validators.maxLength(40) ] ],
      street: [ '', [ Validators.required, Validators.minLength(3), Validators.maxLength(40) ] ]
    });
  }

  private initContactForm() {
    this.contactPersonForm = this.formBuilder.group({
      firstPhonenumber: [
        '',
        {
          validators: [ Validators.required, Validators.minLength(10), Validators.maxLength(10), numericCharacters() ],
          updateOn: 'blur'
        }
      ],
      secondPhonenumber: [ null, [ Validators.minLength(10), Validators.maxLength(10), numericCharacters() ] ],
      firstLandline: [ null, [ Validators.minLength(10), Validators.maxLength(15), numericCharacters() ] ],
      secondLandline: [ null, [ Validators.minLength(10), Validators.maxLength(15), numericCharacters() ] ],
      email: [
        '',
        {
          validators: [ Validators.required, Validators.email ],
          updateOn: 'blur'
        }
      ]
    });
  }

  private initContactPeopleForm() {
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
  }

  private initLegalPersonForm() {
    this.legalPersonForm = this.formBuilder.group({
      NIT: [ null, [ Validators.required, Validators.minLength(5), Validators.maxLength(30), numericCharacters() ] ],
      businessName: [ null, [ Validators.required, Validators.minLength(5), Validators.maxLength(50) ] ]
    });
  }

  private initClientForm() {
    this.clientForm = this.formBuilder.group({
      id: [
        '',
        {
          validators: [ Validators.required, Validators.minLength(8), Validators.maxLength(10), numericCharacters() ],
          updateOn: 'blur'
        }
      ],
      firstName: [
        '',
        [ Validators.required, Validators.minLength(2), Validators.maxLength(20), alphabeticCharacters() ]
      ],
      secondName: [ null, [ Validators.minLength(2), Validators.maxLength(20), alphabeticCharacters() ] ],
      lastName: [
        '',
        [ Validators.required, Validators.minLength(2), Validators.maxLength(20), alphabeticCharacters() ]
      ],
      secondLastname: [ null, [ Validators.minLength(2), Validators.maxLength(20), alphabeticCharacters() ] ],
      tradeName: [ '', [ Validators.required, Validators.minLength(5), Validators.maxLength(50) ] ],
      clientType: [ '', [ Validators.required ] ]
    });
  }

  onSubmit(user: User): void {
    if (user && this.allFormsValid()) {
      this.savingData = true;

      user.email = this.contact_controls['email'].value;
      user.phonenumber = this.contact_controls['firstPhonenumber'].value;

      const client: Client = this.mapClient(user);

      this.clientService.saveClient(client).subscribe((clientRegister) => {
        if (this.authService.userLoggedIn()) {
          this.notificationsService.showSuccessMessage(
            `Cliente ${clientRegister.firstName} ${clientRegister.lastName} registrado con éxito.`,
            () => {
              this.router.navigateByUrl('/clients');
            }
          );
        } else {
          this.notificationsService.showSuccessMessage(
            `Sus datos fueron registrados correctamente y su solicitud fue enviada. Espere nuestra respuesta.`,
            () => {
              this.router.navigateByUrl('/user/login');
            }
          );
        }
      });
    }
  }

  allFormsValid(): boolean {
    return (
      this.clientForm.valid &&
      this.contactPersonForm.valid &&
      this.ubicationForm.valid &&
      (
        this.controls['clientType'].value === 'JuridicPerson' ? this.legalPersonForm.valid :
        true)
    );
  }

  mapClient(user: User): Client {
    const client: Client = {
      ...this.clientForm.value,
      ...this.contactPersonForm.value,
      contactPeople: [
        {
          name: this.contact_people_controls['person_name_1'].value,
          phonenumber: this.contact_people_controls['person_phonenumber_1'].value
        },
        {
          name: this.contact_people_controls['person_name_2'].value,
          phonenumber: this.contact_people_controls['person_phonenumber_2'].value
        }
      ],
      clientAddress: { ...this.ubicationForm.value },
      nit: this.legal_controls['NIT'].value,
      businessName: this.legal_controls['businessName'].value,
      user: user,
      state: ClientState.Pending
    };

    return client;
  }
}
