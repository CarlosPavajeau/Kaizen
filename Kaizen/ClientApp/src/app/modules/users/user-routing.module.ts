import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserLoginComponent } from './components/user-login/user-login.component';
import { UserRegisterComponent } from './components/user-register/user-register.component';
import { UserProfileComponent } from './components/user-profile/user-profile.component';
import { AuthGuard } from 'src/app/core/guards/auth.guard';
import { NoAuthGuard } from 'src/app/core/guards/no-auth.guard';

const routes: Routes = [
  { path: '', children: [
    { path: 'login', component: UserLoginComponent, canActivate: [NoAuthGuard]},
    { path: 'register', component: UserRegisterComponent },
    { path: 'profile', component: UserProfileComponent, canActivate: [AuthGuard] },
    { path: '', redirectTo: 'profile', pathMatch: 'full'}
  ] }
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})
export class UserRoutinModule { }
