import { Component, OnInit } from '@angular/core';
import { ClientService } from '../../services/client.service';
import { FormBuilder, FormGroup, AbstractControl, Validators } from '@angular/forms';
import { IForm } from 'src/app/core/models/form';
import { CharactersValidators } from 'src/app/shared/validators/characters-validators';
import { User } from 'src/app/core/models/user';
import { AuthenticationService } from 'src/app/core/authentication/authentication.service';
import { ClientExistsValidator } from 'src/app/shared/validators/client-exists-validator';

@Component({
  selector: 'app-client-register',
  templateUrl: './client-register.component.html',
  styleUrls: ['./client-register.component.css']
})
export class ClientRegisterComponent implements OnInit, IForm {

  clientForm: FormGroup;
  legalPersonForm: FormGroup;
  contactPersonForm: FormGroup;
  user: User;

  public get controls() : { [key: string]: AbstractControl; } {
    return this.clientForm.controls;
  }

  constructor(
    private clientService: ClientService,
    private authService: AuthenticationService,
    private formBuilder: FormBuilder,
    private clientValidator: ClientExistsValidator
  ) { }

  ngOnInit(): void {
    this.initForm();
  }

  initForm() {
    this.clientForm = this.formBuilder.group({
      id: ['', {
        validators: [Validators.required, Validators.minLength(8), Validators.maxLength(10), CharactersValidators.numericCharacters],
        asyncValidators: [this.clientValidator.validate.bind(this.clientValidator)],
        updateOn: 'blur'
      }],
      firstName: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(20), CharactersValidators.alphabeticCharacters]],
      secondName: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(20), CharactersValidators.alphabeticCharacters]],
      lastName: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(20), CharactersValidators.alphabeticCharacters]],
      secondLastname: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(20), CharactersValidators.alphabeticCharacters]],
      clientType: ['', [Validators.required]]
    });

    this.legalPersonForm = this.formBuilder.group({
      tradeName: ['', [Validators.required]],
      businessName: ['', [Validators.required]]
    });
  }

  onSubmit(user: User)
  {

  }
}
