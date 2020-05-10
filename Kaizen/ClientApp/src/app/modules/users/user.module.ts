import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserRegisterComponent } from './components/user-register/user-register.component';
import { UserLoginComponent } from './components/user-login/user-login.component';
import { UserProfileComponent } from './components/user-profile/user-profile.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from '../../shared/shared.module';
import { UserRoutinModule } from './user-routing.module';
import { ControlPanelComponent } from './components/control-panel/control-panel.component';

@NgModule({
	declarations: [ UserRegisterComponent, UserLoginComponent, UserProfileComponent, ControlPanelComponent ],
	imports: [ FormsModule, ReactiveFormsModule, SharedModule, UserRoutinModule ],
	exports: [ UserRegisterComponent ]
})
export class UserModule {}
