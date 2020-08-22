import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from '@shared/shared.module';
import { ConfirmEmailComponent } from './components/confirm-email/confirm-email.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { ManageDataComponent } from './components/manage-data/manage-data.component';
import { UserLoginComponent } from './components/user-login/user-login.component';
import { UserProfileComponent } from './components/user-profile/user-profile.component';
import { UserRegisterComponent } from './components/user-register/user-register.component';
import { UserRoutinModule } from './user-routing.module';

@NgModule({
  declarations: [
    UserRegisterComponent,
    UserLoginComponent,
    UserProfileComponent,
    DashboardComponent,
    ConfirmEmailComponent,
    ManageDataComponent
  ],
  imports: [ FormsModule, ReactiveFormsModule, SharedModule, UserRoutinModule ],
  exports: [ UserRegisterComponent ]
})
export class UserModule {}
