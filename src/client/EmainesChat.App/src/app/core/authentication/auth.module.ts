import { AuthTokenService } from './auth-token.service';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthService } from './auth.service';

@NgModule({
  declarations: [],
  imports: [
    CommonModule
  ], 
  providers: [
    AuthTokenService,
    AuthService
  ]
})
export class AuthModule { }
