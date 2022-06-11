import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({ providedIn: 'root' })
export class ResTableApiService {
    readonly method = 'table';
    constructor(private httpClient: HttpClient) { }

    getAll() {
        return this.httpClient.get<IResponse<ITable[]>>(this.method);
    }

    getById(id: number) {
        return this.httpClient.get<ITable>(`${this.method}/${id}`);
    }

    updateOrderStatus(value: ITable) {
        return this.httpClient.put(`${this.method}/${value.id}/${value.status}`, value)
    }
}

export interface IResponse<T> {
    count?: number;
    message?: string;
    data: T
}

export interface ITable {
    id: number;
    name: string;
    status: TableStatusEnum;
    capacity: number;

}

export enum TableStatusEnum {
    undefined = 0,
    active = 1,
    inactive = 2,
    available = 3,
    occupied = 4,
}