import { IAuthentication } from './shared/auth.model';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { AuthTokenService } from './auth-token.service';
import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { ChatMessagesService } from 'src/app/Services/ChatServices/chat-messages.service';

@Injectable({
    providedIn: 'root',
})
export class AuthService {
    private readonly apiUrl = `${environment.apiUrl}/`;
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
        private router: Router,
        private chatMessagesService: ChatMessagesService
    ) { }

    public isAuthenticated(): Observable<boolean> {
        return this.http.get<boolean>(`${this.apiUrl}login/isAuthenticated`, {})
            .pipe(
                map(response => !!response),
                catchError(() => of(false))
            );
    }

    public login(data: IAuthentication): Observable<void> {
        // tslint:disable-next-line:prefer-template
        return this.http
            .post(`${this.apiUrl}login`, data, { headers: this.httpHeaders })
            .pipe(
                tap((response: any) => {
                    const token = typeof response === 'string'
                        ? response
                        : response?.token ?? response?.Token ?? '';
                    this.handleAuthToken(true, token);
                    this.chatMessagesService.startConnection();
                })
            );
    }
    public logout(): void {
        this.chatMessagesService.stopConnection();
        this.authTokenService.removeToken();
        this.loggedIn = false;
    }

    private handleAuthToken(loggedIn: boolean, responseOrToken: any): void {
        const token = typeof responseOrToken === 'string'
            ? responseOrToken
            : responseOrToken?.token ?? responseOrToken?.Token ?? '';

        this.loggedIn = loggedIn;
        if (loggedIn && token) {
            this.authTokenService.generateToken(token);
            this.authTokenService.SaveTokenInCookies();
        } else {
            this.authTokenService.resetToken();
        }
    }
}
