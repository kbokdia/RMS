import { Component, OnInit } from '@angular/core';
import { ResTableApiService, TableStatusEnum } from 'src/app/api/res-table-api.service';

@Component({
  selector: 'app-tables',
  templateUrl: './tables.component.html',
  styleUrls: ['./tables.component.scss']
})
export class TablesComponent implements OnInit {

  tables$ = this.tablesApi.getAll();

  readonly TableStatus = TableStatusEnum;

  constructor(private tablesApi: ResTableApiService) {
  }

  ngOnInit(): void {
  }

}
