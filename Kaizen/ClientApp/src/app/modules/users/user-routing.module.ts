import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from '@core/guards/auth.guard';
import { NoAuthGuard } from '@core/guards/no-auth.guard';
import { ConfirmEmailComponent } from './components/confirm-email/confirm-email.component';
import { ManageDataComponent } from './components/manage-data/manage-data.component';
import { UserLoginComponent } from './components/user-login/user-login.component';
import { UserProfileComponent } from './components/user-profile/user-profile.component';

const routes: Routes = [
  {
    path: '',
    children: [
      { path: 'login', component: UserLoginComponent, canActivate: [ NoAuthGuard ] },
      { path: 'profile', component: UserProfileComponent, canActivate: [ AuthGuard ] },
      { path: 'edit', component: ManageDataComponent, canActivate: [ AuthGuard ] },
      { path: 'ConfirmEmail', component: ConfirmEmailComponent },
      { path: '', redirectTo: 'profile', pathMatch: 'full' }
    ]
  }
];

@NgModule({
  declarations: [],
  imports: [ RouterModule.forChild(routes) ],
  exports: [ RouterModule ]
})
export class UserRoutinModule {}
