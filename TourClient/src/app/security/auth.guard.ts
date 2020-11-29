import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { SecurityService } from './security.service';

@Injectable({
  providedIn: 'root'
})
//now inject the security service in the constructor
export class AuthGuard implements CanActivate {
  constructor(private securityService: SecurityService,
    private router: Router){

  }
  //method called from the routing module from canActivate
  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
      let claimType: string = next.data["claimType"];//name of property to be retrieved from the 
      //auth guard class to be accessed using the securityObject[claimType]

      //it's important to match string names from securityObject, authGuard routing module, and here
    if(this.securityService.securityObject.isAuthenticated &&
      this.securityService.securityObject[claimType]) {
        return true;
      }
    else{
      this.router.navigate(['login'],
        { queryParams: { returnUrl: state.url } });
        return false;
    }

  }
  
}
