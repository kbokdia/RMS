import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MenuComponent } from './menu/menu.component';
import { CustomerRoutingModule } from './customer-routing.module';
import { AppMaterialModule } from '../app-material.module';



@NgModule({
  declarations: [
    MenuComponent
  ],
  imports: [
    CommonModule, 
    CustomerRoutingModule,
    AppMaterialModule
  ]
})
export class CustomerModule { }
