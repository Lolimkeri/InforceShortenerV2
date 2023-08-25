import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { TokenProviderService } from '../shared/services/token-provider.service';

@Injectable({
  providedIn: 'root'
})
export class RolesGuard implements CanActivate {

  constructor(private tokenProvider: TokenProviderService,  private router: Router) { }

  canActivate(): boolean {
    if(this.tokenProvider.getRole() == "Admin") {
      this.router.navigate([''])
    }
    
    return true;
  }
}
