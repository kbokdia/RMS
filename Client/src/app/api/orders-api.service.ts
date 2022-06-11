import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({ providedIn: 'root' })
export class OrdersApiService {
    readonly method = 'menu';

    constructor(private httpClient: HttpClient) { }

    getAll() {
        return this.httpClient.get<IOrder[]>(this.method);
    }
}

export interface IOrder {
    id: number;
    userId: number;
    tableId: number;
    specialInstructions: string;
    orderStatus: OrderStatusEnum;
    orderDate: string;
}

export enum OrderStatusEnum {
    undefined = 0,
    active = 1,
    inactive = 2,
    ordered = 3,
    completed = 4,
}


export enum TableStatusEnum {
    undefined = 0,
    active = 1,
    inactive = 2,
    available = 3,
    occupied = 4,
}