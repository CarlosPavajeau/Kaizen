import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserRegisterComponent } from './components/user-register/user-register.component';
import { UserLoginComponent } from './components/user-login/user-login.component';
import { UserProfileComponent } from './components/user-profile/user-profile.component';
import { MaterialModule } from '../core/material.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from '../shared/shared.module';
import { UserRoutinModule } from './user-routin.module';
import { AuthService } from './services/auth.service';
import { CoreModule } from '../core/core.module';



@NgModule({
  declarations: [
    UserRegisterComponent,
    UserLoginComponent,
    UserProfileComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule,
    UserRoutinModule,
  ],
  providers: [AuthService],
  exports: [
    UserRegisterComponent,
    UserLoginComponent,
    UserProfileComponent
  ]
})
export class UserModule { }
