import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { filter, firstValueFrom, lastValueFrom, map } from 'rxjs';
import { ITable, ResTableApiService, TableStatusEnum } from 'src/app/api/res-table-api.service';

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

  constructor(private formBuilder: FormBuilder, private tablesApi: ResTableApiService) {
  }

  async ngOnInit() {
    const tables = await lastValueFrom(this.tablesApi.getAll());

    this.formArray = this.formBuilder.nonNullable.array(tables.data
      .map(table => this.createFormGroup(table)));

    this.formArray.controls.map(x => x.valueChanges
      .pipe(
        filter(value => !!value.status),
        map(value => value as ITable))
      .subscribe(value => this.updateOrderStatus(value)));
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

