import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IResponse } from './res-table-api.service';
@Injectable({ providedIn: 'root' })
export class ResMenuApiService {
    readonly method = 'menu';
    constructor(private httpClient: HttpClient) { }

    getAllByCategories() {
        return this.httpClient.get<IResponse<ICategory[]>>(this.method);
    }
}

export interface IMenuItem {
    id: number;
    name: string;
    categoryType: string;
    price: number;
    description: string;
    imageUrl: string;
    tags: string[];
    isVeg: boolean
    status: boolean
}

export interface ICategory {
    category: string;
    items: IMenuItem[];
}
