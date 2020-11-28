import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { AppUser } from './app-user';
import { AppUserAuth } from './app-user-auth';
import { LOGIN_MOCKS } from './login-mocks';

@Injectable({
  providedIn: 'root'
})
export class SecurityService {
  securityObject: AppUserAuth = new AppUserAuth();

  constructor() { }

  login(entity: AppUser): Observable<AppUserAuth>{
    this.resetSecurityObject();//called from the login and logout ()
    //assigning to, assigning from
    Object.assign(this.securityObject,
      LOGIN_MOCKS.find(login => login.userName.toLowerCase() === entity.userName.toLowerCase()));

    if(this.securityObject.userName !== ""){
      //Store token into local storage
      localStorage.setItem("bearerToken", this.securityObject.bearerToken);
    }
    return of<AppUserAuth>(this.securityObject);
  }

  logout(): void{
    this.resetSecurityObject();
  }

  resetSecurityObject(): void{
    this.securityObject.userName = "";
    this.securityObject.bearerToken = "";
    this.securityObject.isAuthenticated = false;

    this.securityObject.canAccessPaymentDetails = false;
    this.securityObject.canAddPaymentDetails = false;
    this.securityObject.canUpdatePaymentDetails = false;

    localStorage.removeItem("bearerToken");
  }
}
