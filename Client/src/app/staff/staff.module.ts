import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TablesComponent } from './tables/tables.component';
import { StaffRoutingModule } from './staff-routing.module';
import { AppMaterialModule } from '../app-material.module';



@NgModule({
  declarations: [
    TablesComponent
  ],
  imports: [
    CommonModule,
    StaffRoutingModule,
    AppMaterialModule
  ]
})
export class StaffModule { }
