import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import jwt_decode from "jwt-decode";

export const TOKEN_KEY = 'token'
export const ROLE_KEY = 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role'
export const USERNAME_KEY = 'http://schemas.microsoft.com/ws/2008/06/identity/claims/name'

@Injectable({
  providedIn: 'root'
})
export class TokenProviderService {

  constructor(private jwtHelper: JwtHelperService) { }

  getToken(): string {
    return localStorage.getItem(TOKEN_KEY) ?? "";
  }

  getUsername(): string {
    console.log("Here");
    var token = localStorage.getItem(TOKEN_KEY);

    if (token != null)
    {
      var tokenData: object = jwt_decode(token);
      type temp = keyof typeof tokenData;
      let myVar = USERNAME_KEY as temp;      
      return tokenData[myVar];
    }

    return "";
  }

  getRole(): string {
    var token = localStorage.getItem(TOKEN_KEY);

    if (token != null)
    {
      var tokenData: object = jwt_decode(token);
      type temp = keyof typeof tokenData;
      let myVar = ROLE_KEY as temp;      
      return tokenData[myVar];
    }

    return "Unauthorized";
  }
}
