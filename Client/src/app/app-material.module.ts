import { NgModule } from '@angular/core';

import { MatButtonModule } from '@angular/material/button';
import { MatExpansionModule} from '@angular/material/expansion';
import {MatGridListModule} from '@angular/material/grid-list'


const modules = [
    MatButtonModule, 
    MatExpansionModule,
    MatGridListModule
];

@NgModule({
    imports: modules,
    exports: modules,
    providers: [],
})
export class AppMaterialModule { }
