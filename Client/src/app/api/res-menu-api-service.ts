import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
@Injectable({ providedIn: 'root' })
export class ResMenuApiService {
    readonly method = 'menu';
    constructor(private httpClient: HttpClient) { }
    getAllByCategories() {
        return this.httpClient.get<ICategory[]>(this.method);
    }   
}

export interface IMenu{
    id: number;
    name: string;
    categoryType: string;
    price: string;
    description:string;
    imageUrl:string;
    tags:string[];
    isVeg:boolean
    status:boolean
}

export interface ICategory{
    category:string;
    items: IMenu[];
}
