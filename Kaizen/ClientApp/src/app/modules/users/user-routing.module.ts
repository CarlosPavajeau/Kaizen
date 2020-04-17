import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserLoginComponent } from './components/user-login/user-login.component';
import { UserRegisterComponent } from './components/user-register/user-register.component';
import { UserProfileComponent } from './components/user-profile/user-profile.component';

const routes: Routes = [
  { path: '', children: [
    { path: 'login', component: UserLoginComponent},
    { path: 'register', component: UserRegisterComponent },
    { path: 'profile', component: UserProfileComponent },
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
