import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatBottomSheetRef } from '@angular/material/bottom-sheet';
import { lastValueFrom } from 'rxjs';
import { ResUserApiService, Status, UserTypeEnum } from 'src/app/api/res-user-api.service';

@Component({
  selector: 'app-sign-up-form',
  templateUrl: './sign-up-form.component.html',
  styleUrls: ['./sign-up-form.component.scss']
})
export class SignUpFormComponent implements OnInit {

  formGroup: FormGroup<UserForm>;

  constructor(
    private fb: FormBuilder,
    private userApiSvc: ResUserApiService,
    private _bottomSheetRef: MatBottomSheetRef<SignUpFormComponent>,
  ) { }

  ngOnInit(): void {
    this.formGroup = this.fb.nonNullable.group({
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      mobile: ['', [Validators.required, Validators.pattern(/^\d{10}$/)]],
      password: ['', Validators.required],
      type: UserTypeEnum.staff,
      status: Status.active
    });
  }

  cancel() {
    this._bottomSheetRef.dismiss(false);
  }
  async save() {
    const userData = this.formGroup.getRawValue();
    await lastValueFrom(this.userApiSvc.save(userData));
    this._bottomSheetRef.dismiss(true);
  }

}
export interface UserForm {
  name: FormControl<string>,
  email: FormControl<string>,
  mobile: FormControl<string>,
  password: FormControl<string>,
  type: FormControl<number>,
  status: FormControl<number>
}