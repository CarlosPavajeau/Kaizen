import { Component, OnInit } from '@angular/core';
import { ClientService } from '../../services/client.service';
import { FormBuilder, FormGroup, AbstractControl, Validators } from '@angular/forms';
import { IForm } from 'src/app/shared/models/form';
import { CharactersValidators } from 'src/app/shared/validators/characters-validators';

@Component({
  selector: 'app-client-register',
  templateUrl: './client-register.component.html',
  styleUrls: ['./client-register.component.css']
})
export class ClientRegisterComponent implements OnInit, IForm {

  clientForm: FormGroup;

  public get controls() : { [key: string]: AbstractControl; } {
    return this.clientForm.controls;
  }

  constructor(
    private clientService: ClientService,
    private formBuilder: FormBuilder
  ) { }

  ngOnInit(): void {
    this.initForm();
  }

  initForm() {
    this.clientForm = this.formBuilder.group({
      id: ['', [Validators.required, Validators.minLength(8), Validators.maxLength(10), CharactersValidators.numericCharacters]],
      firstName: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(20), CharactersValidators.alphabeticCharacters]],
      secondName: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(20), CharactersValidators.alphabeticCharacters]],
      lastName: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(20), CharactersValidators.alphabeticCharacters]],
      secondLastname: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(20), CharactersValidators.alphabeticCharacters]]
    });
  }

  onClick() {
    console.log(this.controls['id'].errors);
  }

}
