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

    updateMenuItemStatus(value: IMenuItem) {
        return this.httpClient.put(`${this.method}/${value.id}/${value.status}`, value);
    }
}

export interface IMenuItem {
    id: number;
    name: string;
    status: MenuItemStatusEnum;
    categoryType: string;
    price: number;
    description: string;
    imageUrl: string;
    // tags: string[];
    isVeg: boolean
}

export interface ICategory {
    category: string;
    items: IMenuItem[];
}

export enum MenuItemStatusEnum {
    undefined = 0,
    active = 1,
    inactive = 2,
}



