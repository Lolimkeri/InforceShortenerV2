import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from '../services/auth.service';
import { TokenProviderService } from '../services/token-provider.service';

@Injectable()
export class AuthorizationInterceptor implements HttpInterceptor {

  constructor(private tps: TokenProviderService) {}

  intercept(req: HttpRequest<any>, next: HttpHandler) {
    // Get the auth token from the service.
    const authToken = this.tps.getToken();

    // Clone the request and replace the original headers with
    // cloned headers, updated with the authorization.
    const authReq = req.clone({
      headers: req.headers.set('Authorization', "bearer " + authToken)
    });

    // send cloned request with header to the next handler.
    return next.handle(authReq);
  }
}
