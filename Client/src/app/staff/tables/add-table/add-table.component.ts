import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatBottomSheetRef } from '@angular/material/bottom-sheet';
import { lastValueFrom } from 'rxjs';
import { ResTableApiService, TableStatusEnum } from 'src/app/api/res-table-api.service';
import { PlaceOrderComponent } from 'src/app/customer/cart/place-order/place-order.component';

@Component({
  selector: 'app-add-table',
  templateUrl: './add-table.component.html',
  styleUrls: ['./add-table.component.scss']
})
export class AddTableComponent implements OnInit {

  formGroup: FormGroup<TableCreateForm>;

  constructor(
    private fb: FormBuilder,
    private _bottomSheetRef: MatBottomSheetRef<PlaceOrderComponent>,
    private rmsTableAPISvc: ResTableApiService,
  ) { }

  ngOnInit(): void {
    this.formGroup = this.fb.nonNullable.group({
      name: ['', Validators.required],
      capacity: [0, [Validators.required, Validators.min(1), Validators.max(99)]],
      status: [TableStatusEnum.inactive]
    })
  }

  async createTable() {
    const formValue = this.formGroup.getRawValue();
    console.log(formValue)
    const response = await lastValueFrom(this.rmsTableAPISvc.save(formValue));
    console.log(response)
    this._bottomSheetRef.dismiss(true);
  }

}

export interface TableCreateForm {
  name: FormControl<string>,
  capacity: FormControl<number>,
  status: FormControl<TableStatusEnum>
}