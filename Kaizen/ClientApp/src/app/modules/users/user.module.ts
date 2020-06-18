import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserRegisterComponent } from './components/user-register/user-register.component';
import { UserLoginComponent } from './components/user-login/user-login.component';
import { UserProfileComponent } from './components/user-profile/user-profile.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from '../../shared/shared.module';
import { UserRoutinModule } from './user-routing.module';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { ConfirmEmailComponent } from './components/confirm-email/confirm-email.component';

@NgModule({
	declarations: [ UserRegisterComponent, UserLoginComponent, UserProfileComponent, DashboardComponent, ConfirmEmailComponent ],
	imports: [ FormsModule, ReactiveFormsModule, SharedModule, UserRoutinModule ],
	exports: [ UserRegisterComponent ]
})
export class UserModule {}
