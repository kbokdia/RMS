import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { of, Subject } from 'rxjs';
import { Router } from '@angular/router';

@Injectable({
    providedIn: "root"
})

export class AuthService {
    // readonly method = 'user';
    private isAuthenticated = false;
    private userType: AuthTypeEnum = AuthTypeEnum.undefined;
    private token: string;
    private username: string;
    private authStatusListener = new Subject<boolean>();


    constructor(public http: HttpClient, private router: Router) {
    }

    getToken() {
        return this.token;
    }

    getIsAuth() {
        return this.isAuthenticated;
    }

    getAuthStatusListner() {
        return this.authStatusListener.asObservable();
    }

    getUsername() {
        return this.username;
    }

    getUserType() {
        return this.userType;
    }

    login(username: string, password: string) {
        const authData = { mobile: username, password: password };
        this.http.post<IAuthResponse>('user/authenticate', authData)
            .subscribe(response => {
                this.token = response.data.token;
                if (!this.token && !response.data.user) return;
                this.isAuthenticated = true;
                this.username = response.data.user.mobile;
                this.userType = response.data.user.type;
                this.authStatusListener.next(true);
                this.saveAuthData(this.token, response.data.user?.mobile, response.data.user.type);

                if (response.data.user.type === AuthTypeEnum.customer) {
                    this.router.navigate(['/customer/menu']);
                }
                if (response.data.user.type === AuthTypeEnum.staff) {
                    this.router.navigate(['/staff']);
                }
            });
    }

    private saveAuthData(token: string, username: string, userType: AuthTypeEnum) {
        localStorage.setItem('token', token);
        localStorage.setItem('username', username);
        localStorage.setItem('userType', userType.toLocaleString());
    }

    autoAuthUser() {
        const authInformation = this.getAuthData();
        if (!authInformation) {
            return;
        }
        this.isAuthenticated = true;
        this.username = authInformation.username ?? "";
        this.userType = authInformation.userType ?? AuthTypeEnum.undefined;

        this.authStatusListener.next(true);

    }

    logout() {
        this.token = "";
        this.isAuthenticated = false;
        this.authStatusListener.next(false);
        this.username = "";
        this.userType = AuthTypeEnum.undefined;
        this.clearAuthData();
        this.router.navigate(['/']);
    }

    private clearAuthData() {
        localStorage.removeItem('token');
        localStorage.removeItem('username');
        localStorage.removeItem('userType');
    }

    private getAuthData() {
        const token = localStorage.getItem('token');
        const username = localStorage.getItem('username');
        const userType = parseInt(localStorage.getItem('userType') ?? '0');
        if (!token && !username && !userType) {
            return;
        }
        return {
            token: token,
            username: username,
            userType: userType
        }
    }

}

export interface IAuthResponse {
    message: string;
    data: IAuthData;
}

export interface IAuthData {
    token: string;
    user: IAuthUser;
}

export interface IAuthUser {
    id: string;
    name: string;
    email: string;
    mobile: string;
    password: string;
    type: AuthTypeEnum;
    status: string;
}

export enum AuthTypeEnum {
    undefined = 0,
    customer = 1,
    staff = 2,
    admin = 3
}