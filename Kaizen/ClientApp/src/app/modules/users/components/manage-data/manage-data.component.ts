import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { User } from '@app/core/models/user';
import { NotificationsService } from '@app/shared/services/notifications.service';
import { AuthenticationService } from '@core/authentication/authentication.service';
import { IForm } from '@core/models/form';
import { ChangePasswordModel } from '@modules/users/models/change-password';
import { Person } from '@shared/models/person';
import { HttpErrorHandlerService } from '@shared/services/http-error-handler.service';
import { of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-manage-data',
  templateUrl: './manage-data.component.html',
  styleUrls: [ './manage-data.component.css' ]
})
export class ManageDataComponent implements OnInit, IForm {
  changePasswordForm: FormGroup;
  currentPerson: Person;
  currentUserType: string;
  hideOldPassword = true;
  hideNewPassword = true;

  updatingData = false;

  get controls(): { [key: string]: AbstractControl } {
    return this.changePasswordForm.controls;
  }

  constructor(
    private userService: UserService,
    private formBuilder: FormBuilder,
    private authService: AuthenticationService,
    private notificationsService: NotificationsService,
    private errorHandlerService: HttpErrorHandlerService
  ) {}

  ngOnInit(): void {
    this.loadData();
    this.initForm();
  }

  private loadData(): void {
    this.currentPerson = JSON.parse(localStorage.getItem('current_person'));
    this.currentUserType = this.authService.getUserRole();
  }

  initForm(): void {
    this.changePasswordForm = this.formBuilder.group({
      oldPassword: [ '', [ Validators.required, Validators.minLength(8), Validators.maxLength(30) ] ],
      newPassword: [ '', [ Validators.required, Validators.minLength(8), Validators.maxLength(30) ] ]
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
}
