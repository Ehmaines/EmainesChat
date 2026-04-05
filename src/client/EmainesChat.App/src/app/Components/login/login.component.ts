import { IAuthentication } from './../../core/authentication/shared/auth.model';
import { AuthTokenService } from './../../core/authentication/auth-token.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from './../../core/authentication/auth.service';
import { FormControl } from '@angular/forms';
import { Subject } from 'rxjs';
@Component({
    selector: 'ehm-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
    showErrorMessage = false;
    password = new FormControl('');
    email = new FormControl('');
    private ngUnsubscribe: Subject<void> = new Subject<void>();
    public loginData: IAuthentication = {
        username: '',
        email: '',
        password: '',
    };
    constructor(
        private router: Router,
        private route: ActivatedRoute,
        private authService: AuthService,
        private authTokenService: AuthTokenService,
    ) { }

    ngOnInit(): void { }

    public ngOnDestroy(): void {
        this.ngUnsubscribe.next();
        this.ngUnsubscribe.complete();
    }

    login(event: Event) {
        event.preventDefault();
        this.loginData.email = this.email.value ? this.email.value : '';
        this.loginData.password = this.password.value
            ? this.password.value
            : '';

        this.authService
            .login(this.loginData)
            .subscribe({
                next: (): void => {
                    this.showErrorMessage = false;
                    this.router.navigate(['/home']);
                },
                error: (reason: any): void => {
                    console.log(reason)
                    this.showErrorMessage = true;
                },
            });
    }
}
