import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatBottomSheetRef } from '@angular/material/bottom-sheet';

@Component({
  selector: 'app-place-order',
  templateUrl: './place-order.component.html',
  styleUrls: ['./place-order.component.scss']
})
export class PlaceOrderComponent implements OnInit {

  @ViewChild("inputNumber") inputNumber: ElementRef;
  formGroup: FormGroup;
  phoneNumberForm: FormControl<string>;
  constructor(
    private fb: FormBuilder,
    private _bottomSheetRef: MatBottomSheetRef<PlaceOrderComponent>
  ) { }

  ngOnInit(): void {
    this.formGroup = this.fb.nonNullable.group({
      phoneNumber: ['', [Validators.required, Validators.pattern(/^\d{10}$/)]],
    })
  }

  closeForm() {
    const userInputValue = this.formGroup.value?.phoneNumber || '';
    this._bottomSheetRef.dismiss(userInputValue);
  }
}
