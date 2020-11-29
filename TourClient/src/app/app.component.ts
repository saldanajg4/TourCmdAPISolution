import { Component } from '@angular/core';

import {MenuItem} from 'primeng/api';
import { Router, RouterLink } from '@angular/router';
import { AppUserAuth } from './security/app-user-auth';
import { SecurityService } from './security/security.service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  /**
   *
   */
  constructor(private router: Router, private securityService: SecurityService) {
    this.securityObject = this.securityService.securityObject;
  }
  title = 'TourClient';
  items: MenuItem[];
  securityObject: AppUserAuth = null;

    ngOnInit() {
        this.items = [
            {label: 'Tours', icon: 'fa fa-fw fa-bar-chart',
              command: (event) =>{
              console.log(event.item.label);
              this.router.navigate(['/tour']);
            }},
            {label: 'Orders', icon: 'fa fa-fw fa-calendar',  routerLink: ['/order']},

            {label: 'Items', icon: 'fa fa-fw fa-book', command: (event) =>{
              console.log(event);
              this.router.navigateByUrl('/menu-items');
            }},

            {label: 'Ingredients', icon: 'fa fa-fw fa-support', command: (event) =>{
              console.log(event);
              this.router.navigateByUrl('/ingredients');
            }},
            {label: 'Payment', icon: 'fa fa-fw fa-twitter', command: (event) =>{
              console.log(event);
              if(this.securityObject.canAccessPaymentDetails){
                this.router.navigateByUrl('/pay-details');
              }
              
            }}
        ];
    }
    logout(){
      this.securityService.logout();
    }
}
