import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login/login.component';
import { AuthRoutingModule } from './auth-routing.module';
import { AppMaterialModule } from '../app-material.module';
import { SharedModule } from '../shared/shared.module';
import { ReactiveFormsModule } from '@angular/forms';
import { SignUpFormComponent } from './login/sign-up-form/sign-up-form.component';



@NgModule({
  declarations: [
    LoginComponent,
    SignUpFormComponent
  ],
  imports: [
    CommonModule,
    AuthRoutingModule,
    AppMaterialModule,
    SharedModule,
    ReactiveFormsModule,
  ]
})
export class AuthModule { }
