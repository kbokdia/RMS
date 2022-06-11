import { CanActivate, Router } from '@angular/router';
import { Injectable } from '@angular/core';
import { AuthService, AuthTypeEnum } from './auth.service';


@Injectable()
export class AuthGuard implements CanActivate {
    constructor(private authSvc: AuthService, private rotuer: Router) { }
    canActivate(route: import("@angular/router").ActivatedRouteSnapshot, state: import("@angular/router").RouterStateSnapshot): boolean | import("@angular/router").UrlTree | import("rxjs").Observable<boolean | import("@angular/router").UrlTree> | Promise<boolean | import("@angular/router").UrlTree> {
        const isAuth = this.authSvc.getIsAuth();
        if (!isAuth) {
            this.rotuer.navigate(['/auth/login']);
        }

        const configPath = route.routeConfig?.path;
        const userType = this.authSvc.getUserType()
        if (userType === AuthTypeEnum.staff && configPath !== 'staff') {
            this.rotuer.navigate(['/staff']);
        }
        if (userType === AuthTypeEnum.customer && configPath !== 'customer') {
            this.rotuer.navigate(['/auth/login']);
        }

        return isAuth;
    }
}