import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { MatBottomSheet } from '@angular/material/bottom-sheet';
import { AuthService } from '../auth.service';
import { SignUpFormComponent } from './sign-up-form/sign-up-form.component';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  formGroup = this.formBuilder.nonNullable.group({
    username: ['', Validators.required],
    password: ['', Validators.required]
  })

  constructor(
    private formBuilder: FormBuilder,
    private authSvc: AuthService,
    private _bottomSheet: MatBottomSheet,
  ) { }

  ngOnInit(): void {
  }

  onLogin() {
    const value = this.formGroup.getRawValue();
    this.authSvc.login(value.username, value.password);
  }

  ngOnDestroy() {
    // this.authStatusSub.unsubscribe();
  }
  openSignUp(event: Event) {
    event.preventDefault();
    event.stopImmediatePropagation();
    this._bottomSheet.open(SignUpFormComponent, { disableClose: true });
  }

}
