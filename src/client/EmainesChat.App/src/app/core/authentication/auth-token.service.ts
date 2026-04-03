import { Roles } from './shared/roles.enum';
import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { IAuthenticationToken } from './shared/auth-token.model';
import { CookieService } from 'ngx-cookie-service';

@Injectable({
    providedIn: 'root',
})
export class AuthTokenService {
    private jwtHelper: JwtHelperService = new JwtHelperService();
    private authToken: string = '';
    private decodedToken: IAuthenticationToken = {
        role: 0,
        name: '',
        email: ''
    };
    constructor(private cookieService: CookieService) { }

    public get encodedToken(): string {
        if (this.authToken) {
            return this.authToken;
        }

        return this.cookieService.get('Authorization') || '';
    }

    public get token(): IAuthenticationToken {
        if (!this.decodedToken && this.authToken) {
            this.decodedToken = this.jwtHelper.decodeToken(this.authToken)!;
        }

        return this.decodedToken || <IAuthenticationToken>{};
    }

    public get role(): Roles {
        return +this.token.role;
    }

    public generateToken(value: any): void {
        const token = typeof value === 'string' ? value : value?.token ?? value?.Token ?? '';
        this.authToken = token;
        this.decodedToken = token ? this.decodeToken(token) : { role: 0, name: '', email: '' };
    }

    public resetToken(): void {
        this.authToken = '';
        this.decodedToken = {
            role: 0,
            name: '',
            email: ''
        };
    }

    public decodeToken(value: any): IAuthenticationToken {
        if (!value || typeof value !== 'string') {
            return { role: 0, name: '', email: '' };
        }
        try {
            return this.jwtHelper.decodeToken(value) as IAuthenticationToken;
        } catch {
            return { role: 0, name: '', email: '' };
        }
    }

    public SaveTokenInCookies() {
        const expires = new Date();
        expires.setTime(expires.getTime() + (8 * 60 * 60 * 1000)); //expira o cookie em 8 horas
        this.cookieService.set('Authorization', this.authToken, expires)
    }
}
