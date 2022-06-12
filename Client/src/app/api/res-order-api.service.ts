import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IAuthUser } from '../auth/auth.service';
import { IMenuItem } from './res-menu-api-service';
import { IResponse, ITable } from './res-table-api.service';

@Injectable({
  providedIn: 'root'
})
export class ResOrderApiService {
  readonly method = 'order';
  constructor(private httpClient: HttpClient,) { }

  getAll() {
    return this.httpClient.get<IOrderGet>(this.method);
  }

  save(order: IOrder) {
    return this.httpClient.post<IResponse<{ orderId: number }>>(`${this.method}`, order);
  }

  updateStatus(id: number, status: OrderEnum) {
    return this.httpClient.put(`${this.method}/${id}/${status}`, id);
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


export interface IOrderGet {
  message: string;
  count: number;
  data: IOrderData[];
}

export interface IOrderData {
  id: number;
  orderDatetime: string;
  user: IAuthUser;
  table: ITable;
  items: IOrderItem[]
}

export interface IOrderItem {
  id: number;
  menuId: number;
  quantity: number;
  menu: IMenuItem;
}