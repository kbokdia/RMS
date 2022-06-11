import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IMenuItem } from './res-menu-api-service';
import { IResponse } from './res-table-api.service';

@Injectable({
  providedIn: 'root'
})
export class ResOrderApiService {
  readonly method = 'order';
  constructor(private httpClient: HttpClient,) { }

  save(order: IOrder) {
    return this.httpClient.post<IResponse<{orderId: number}>>(`${this.method}`, order);
  }
}

export enum OrderEnum {
  Undefined = 0,
  Active = 1,
  Inactive = 2,
  Pending = 3,
  Completed = 4
}

export interface IOrder {
  mobile: string;
  userId: number;
  tableId: number;
  instructions: string;
  status: OrderEnum;
  items: IMenuItemRef[];
}

export interface IMenuItemRef {
  menuId: number;
  quantity: number;
}