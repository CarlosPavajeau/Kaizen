import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardLayoutComponent } from '@app/shared/layouts/dashboard-layout/dashboard-layout.component';
import { AuthGuard } from '@core/guards/auth.guard';
import { NoAuthGuard } from '@core/guards/no-auth.guard';
import { ConfirmEmailComponent } from './components/confirm-email/confirm-email.component';
import { ForgottenPasswordComponent } from './components/forgotten-password/forgotten-password.component';
import { ManageDataComponent } from './components/manage-data/manage-data.component';
import { ResetPasswordComponent } from './components/reset-password/reset-password.component';
import { UserLoginComponent } from './components/user-login/user-login.component';
import { UserProfileComponent } from './components/user-profile/user-profile.component';

const routes: Routes = [
  {
    path: '',
    component: DashboardLayoutComponent,
    children: [
      { path: 'profile', component: UserProfileComponent, canActivate: [ AuthGuard ] },
      { path: 'edit', component: ManageDataComponent, canActivate: [ AuthGuard ] },
      { path: '', redirectTo: 'profile', pathMatch: 'full' }
    ]
  },
  { path: 'login', component: UserLoginComponent, canActivate: [ NoAuthGuard ] },
  { path: 'ResetPassword', component: ResetPasswordComponent, canActivate: [ NoAuthGuard ] },
  { path: 'ConfirmEmail', component: ConfirmEmailComponent },
  { path: 'forgotten-password', component: ForgottenPasswordComponent, canActivate: [ NoAuthGuard ] }
];

@NgModule({
  declarations: [],
  imports: [ RouterModule.forChild(routes) ],
  exports: [ RouterModule ]
})
export class UserRoutingModule {}
