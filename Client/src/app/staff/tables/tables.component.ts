import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { MatBottomSheet } from '@angular/material/bottom-sheet';
import { filter, firstValueFrom, lastValueFrom, map } from 'rxjs';
import { ITable, ResTableApiService, TableStatusEnum } from 'src/app/api/res-table-api.service';
import { AddTableComponent } from './add-table/add-table.component';

@Component({
  selector: 'app-tables',
  templateUrl: './tables.component.html',
  styleUrls: ['./tables.component.scss']
})
export class TablesComponent implements OnInit {
  formArray: FormArray<FormGroup<TablesFormModel>>;
  readonly TableStatus = TableStatusEnum;
  readonly TableStatusKeys = Object.values(TableStatusEnum)
    .filter(value => typeof value === 'number' && value > 0) as number[];

  constructor(
    private formBuilder: FormBuilder,
    private tablesApi: ResTableApiService,
    private _bottomSheet: MatBottomSheet,
  ) {
  }

  async ngOnInit() {
    await this.fetchData();
    this.formArray.controls.map(x => x.valueChanges
      .pipe(
        filter(value => !!value.status),
        map(value => value as ITable))
      .subscribe(value => this.updateOrderStatus(value)));
  }

  async fetchData() {
    const tables = await lastValueFrom(this.tablesApi.getAll());
    this.formArray = this.formBuilder.nonNullable.array(tables.data
      .map(table => this.createFormGroup(table)));

  }

  createFormGroup(table: ITable) {
    const formGroup: FormGroup<TablesFormModel> = this.formBuilder.nonNullable.group({
      id: table.id,
      name: table.name,
      capacity: table.capacity,
      status: table.status
    });
    return formGroup;
  }

  async openAddPopUp() {
    const bottomSheetPopUp = this._bottomSheet.open(AddTableComponent);
    const hasCreated = await lastValueFrom(bottomSheetPopUp.afterDismissed());
    if (hasCreated) {
      await this.fetchData();
    }
  }

  private updateOrderStatus(value: ITable) {
    this.tablesApi.updateOrderStatus(value).subscribe()
  }
}

interface TablesFormModel {
  id: FormControl<number>;
  name: FormControl<string>;
  capacity: FormControl<number>;
  status: FormControl<TableStatusEnum>;
}

