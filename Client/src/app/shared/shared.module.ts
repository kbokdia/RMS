import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HoverButtonComponent } from './hover-button/hover-button.component';



@NgModule({
  declarations: [
    HoverButtonComponent
  ],
  imports: [
    CommonModule
  ],
  exports: [
    HoverButtonComponent
  ]
})
export class SharedModule { }
