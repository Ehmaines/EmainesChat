import { Roles } from './shared/roles.enum';
import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { IAuthenticationToken } from './shared/auth-token.model';

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
    constructor() {}

    public get encodedToken(): string {
        return this.authToken;
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

    public generateToken(value: string): void {
        this.authToken = value;
        this.decodedToken = this.jwtHelper.decodeToken(value)!;
        console.log("---------------------Generated token---------------------")
        console.log(this.authToken)
        console.log("---------------------FIM Generated token---------------------")
    }

    public resetToken(): void {
        this.authToken = '';
        this.decodedToken = {
            role: 0,
            name: '',
            email: ''
        };
    }
}
