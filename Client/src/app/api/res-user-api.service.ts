import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ResUserApiService {

  readonly method = 'user';
  constructor(private httpClient: HttpClient) { }

  save(data: IUser) {
    return this.httpClient.post(`${this.method}`, data)
  }
}

export interface IUser {
  name: string,
  email: string,
  mobile: string,
  password: string,
  type: UserTypeEnum,
  status: Status
}
export enum UserTypeEnum {
  undefined = 0,
  customer = 1,
  staff = 2,
  admin = 3,
}
export enum Status {
  undefined = 0,
  active = 1,
  inactive = 2,
}
