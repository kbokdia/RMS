import { NgModule } from '@angular/core';

import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatSelectModule } from '@angular/material/select';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatDividerModule } from '@angular/material/divider';
import { MatListModule } from '@angular/material/list';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';


const modules = [
    MatButtonModule,
    MatCardModule,
    MatSelectModule,
    MatGridListModule,
    MatButtonToggleModule,
    MatExpansionModule,
    MatSlideToggleModule,
    MatDividerModule,
    MatListModule,
    MatProgressSpinnerModule
];

@NgModule({
    imports: modules,
    exports: modules,
    providers: [],
})
export class AppMaterialModule { }
