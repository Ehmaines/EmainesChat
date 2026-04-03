import { AuthTokenService } from './auth-token.service';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthService } from './auth.service';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthInterceptor } from './auth.interceptor';

@NgModule({
  declarations: [],
  imports: [
    CommonModule
  ], 
  providers: [
    {
      provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true
    },
    AuthTokenService,
    AuthService
  ]
})
export class AuthModule { }
