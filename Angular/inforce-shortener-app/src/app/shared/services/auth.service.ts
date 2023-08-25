import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Observable, tap } from 'rxjs';
import { Token } from '../models/token';
import { environment } from '../../../environments/environment';

export const TOKEN_KEY = 'token'

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private baseUrl: string = environment.baseUrl;
  
  private loginUrl: string = this.baseUrl + '/user/login';
  private registerUrl: string = this.baseUrl + '/user/register';

  constructor(
    private http: HttpClient,
    private jwtHelper: JwtHelperService,
    private router: Router
  ) { }

  register(username: string, password: string): Observable<Token> {
    return this.http.post<Token>(this.registerUrl, {username, password}).pipe(tap(token => {localStorage.setItem(TOKEN_KEY, token.token);}));
  }

  login(username: string, password: string): Observable<Token> {
    return this.http.post<Token>(this.loginUrl, {username, password}).pipe(tap(token => {localStorage.setItem(TOKEN_KEY, token.token);}))
  }

  isAuthenticated(): boolean {
    var token = localStorage.getItem(TOKEN_KEY);
    return (token != null) && !this.jwtHelper.isTokenExpired(token)
  }

  logout(isRedirect: boolean = true): void {
    localStorage.removeItem(TOKEN_KEY);

    if(isRedirect)
    {
      this.router.navigate(['login']);
    }
  }
}
