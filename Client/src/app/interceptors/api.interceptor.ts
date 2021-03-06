import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';


@Injectable()
export class ApiInterceptor implements HttpInterceptor {

    private baseUrl = environment.baseApiUrl;

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const apiUrl = `${this.baseUrl}/${req.url}`;
        const parsedApiUrl = apiUrl;
        req = req.clone({
            url: parsedApiUrl,
            setHeaders: {
                'Content-Security-Policy': `frame-ancestors ${environment.security.allowedOrigins}`,
                'X-Frame-Options': `ALLOW-FROM ${environment.security.allowedOrigins}`,
                'X-XSS-Protection': '1; mode=block'
            }
        });

        return next.handle(req);
    }
}