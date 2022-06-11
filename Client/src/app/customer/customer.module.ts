import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MenuComponent } from './menu/menu.component';
import { CustomerRoutingModule } from './customer-routing.module';
import { AppMaterialModule } from '../app-material.module';
import { CartComponent } from './cart/cart.component';
import { ReactiveFormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    MenuComponent,
    CartComponent
  ],
  imports: [
    CommonModule, 
    CustomerRoutingModule,
    AppMaterialModule,
    ReactiveFormsModule,
  ]
})
export class CustomerModule { }
