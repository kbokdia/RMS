import { NgModule } from '@angular/core';

import { MatButtonModule } from '@angular/material/button';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatCardModule } from '@angular/material/card';
import { MatDividerModule } from '@angular/material/divider';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatInputModule } from '@angular/material/input';
import { MatListModule } from '@angular/material/list';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSelectModule } from '@angular/material/select';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';


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
    MatProgressSpinnerModule,
    MatInputModule,
    MatFormFieldModule
];

@NgModule({
    imports: modules,
    exports: modules,
    providers: [],
})
export class AppMaterialModule { }
