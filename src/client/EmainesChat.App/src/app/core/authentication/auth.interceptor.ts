import { Injectable } from '@angular/core';
import {
    HttpRequest,
    HttpHandler,
    HttpEvent,
    HttpInterceptor,
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthTokenService } from './auth-token.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
    constructor(private authTokenService: AuthTokenService) { }

    public intercept(
        request: HttpRequest<unknown>,
        next: HttpHandler
    ): Observable<HttpEvent<unknown>> {
        const token = this.authTokenService.encodedToken;
        if (token) {
            const authorizationHeader = token.startsWith('Bearer ')
                ? token
                : `Bearer ${token}`;
            request = request.clone({
                setHeaders: {
                    Authorization: authorizationHeader,
                },
            });
        }
        return next.handle(request);
    }
}
