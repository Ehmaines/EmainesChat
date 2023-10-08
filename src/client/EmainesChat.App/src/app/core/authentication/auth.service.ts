import { IAuthentication } from './shared/auth.model';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { AuthTokenService } from './auth-token.service';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';

@Injectable({
    providedIn: 'root',
})
export class AuthService {
    private readonly apiUrl: string = 'https://localhost:7080/';
    private httpHeaders: HttpHeaders = new HttpHeaders({
        'Content-Type': 'application/json',
    });
    private status: boolean = false;

    public get loggedIn(): boolean {
        return this.status;
    }

    public set loggedIn(status: boolean) {
        this.status = status;
    }

    constructor(
        private http: HttpClient,
        private authTokenService: AuthTokenService,
        private router: Router
    ) {}

    public login(data: IAuthentication): Observable<void> {
        // tslint:disable-next-line:prefer-template
        return this.http
            .post(`${this.apiUrl}login`, data, { headers: this.httpHeaders })
            .pipe(
                tap((response: any) => {
                    debugger;
                    console.log(response)
                    this.handleAuthToken(true, response);
                })
            );
    }

    private handleAuthToken(loggedIn: boolean, token: string): void {
        this.loggedIn = loggedIn;
        if (loggedIn) {
            this.authTokenService.generateToken(token);
        } else {
            this.authTokenService.resetToken();
        }
    }
}
