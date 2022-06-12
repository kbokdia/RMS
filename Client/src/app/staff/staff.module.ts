import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TablesComponent } from './tables/tables.component';
import { StaffRoutingModule } from './staff-routing.module';
import { AppMaterialModule } from '../app-material.module';
import { OrdersComponent } from './orders/orders.component';
import { MenuComponent } from './menu/menu.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HomeComponent } from './home/home.component';
import { AddTableComponent } from './tables/add-table/add-table.component';



@NgModule({
  declarations: [
    TablesComponent,
    OrdersComponent,
    MenuComponent,
    HomeComponent,
    AddTableComponent
  ],
  imports: [
    CommonModule,
    StaffRoutingModule,
    AppMaterialModule,
    ReactiveFormsModule
  ]
})
export class StaffModule { }
