import { NgModule } from '@angular/core';

import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatSelectModule } from '@angular/material/select';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
// import { MatGridList } from '@angular/material/gir';


const modules = [
    MatButtonModule,
    MatCardModule,
    MatSelectModule,
    MatGridListModule,
    MatButtonToggleModule
];

@NgModule({
    imports: modules,
    exports: modules,
    providers: [],
})
export class AppMaterialModule { }
