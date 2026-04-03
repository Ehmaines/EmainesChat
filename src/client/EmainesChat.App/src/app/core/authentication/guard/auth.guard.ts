import { AuthService } from './../auth.service';
import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { of, map, take, catchError, Observable  } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {
    constructor(private router: Router, private cookieService: CookieService, private authService: AuthService) {}

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> {
        const token = this.cookieService.get('Authorization');
        if (!token) {
            this.router.navigate(['/login']);
            return of(false);
        }

        return this.authService.isAuthenticated().pipe(
            take(1),
            map(isValid => {
                if (isValid) {
                    return true;
                } else {
                    this.router.navigate(['/login']);
                    return false;
                }
            }),
            catchError(() => {
                this.router.navigate(['/login']);
                return of(false);
            })
        );
    }
}
