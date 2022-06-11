import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MenuComponent } from './menu/menu.component';
import { CustomerRoutingModule } from './customer-routing.module';
import { AppMaterialModule } from '../app-material.module';
import { CartComponent } from './cart/cart.component';



@NgModule({
  declarations: [
    MenuComponent,
    CartComponent
  ],
  imports: [
    CommonModule, 
    CustomerRoutingModule,
    AppMaterialModule
  ]
})
export class CustomerModule { }
