import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { StatisticsModule } from '@modules/statistics/statistics.module';
import { SharedModule } from '@shared/shared.module';
import { ConfirmEmailComponent } from './components/confirm-email/confirm-email.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { ForgottenPasswordComponent } from './components/forgotten-password/forgotten-password.component';
import { ManageDataComponent } from './components/manage-data/manage-data.component';
import { ResetPasswordComponent } from './components/reset-password/reset-password.component';
import { UserLoginComponent } from './components/user-login/user-login.component';
import { UserProfileComponent } from './components/user-profile/user-profile.component';
import { UserRegisterComponent } from './components/user-register/user-register.component';
import { UserRoutingModule } from './user-routing.module';

@NgModule({
  declarations: [
    UserRegisterComponent,
    UserLoginComponent,
    UserProfileComponent,
    DashboardComponent,
    ConfirmEmailComponent,
    ManageDataComponent,
    ForgottenPasswordComponent,
    ResetPasswordComponent
  ],
  imports: [ FormsModule, ReactiveFormsModule, SharedModule, UserRoutingModule, StatisticsModule ],
  exports: [ UserRegisterComponent ]
})
export class UserModule {}
