import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class LocalStorageService {
  private readonly prefix: string = "RMS"
  constructor() { }

  private getKey = (key: string) => `${this.prefix}_${key}` || '';

  getValue(key: string) {
    const value = localStorage.getItem(this.getKey(key)) || '';
    return JSON.parse(value);
  }

  setValue(key: string, value: any) {
    const prefixedKey = this.getKey(key);
    return localStorage.setItem(prefixedKey, JSON.stringify(value || ''));
  }
}
