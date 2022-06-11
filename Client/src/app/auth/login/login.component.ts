import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { AuthService } from '../auth.service';

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

  constructor(private formBuilder: FormBuilder, private authSvc: AuthService) { }

  ngOnInit(): void {
  }

  onLogin() {
    const value = this.formGroup.getRawValue();
    this.authSvc.login(value.username, value.password);
  }

  ngOnDestroy() {
    // this.authStatusSub.unsubscribe();
  }


}
