import { Component } from '@angular/core';
import {TabMenuModule} from 'primeng/tabmenu';
import {MenuItem} from 'primeng/api';
import { Router, RouterLink } from '@angular/router';
import {MenuModule} from 'primeng/menu';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  /**
   *
   */
  constructor(private router: Router) {
  }
  title = 'TourClient';
  items: MenuItem[];

    ngOnInit() {
        this.items = [
            {label: 'Stats', icon: 'fa fa-fw fa-bar-chart',
              command: (event) =>{
              console.log(event.item.label);
              this.router.navigate(['/tour']);
            }},
            {label: 'Calendar', icon: 'fa fa-fw fa-calendar',  routerLink: ['/tour']},

            {label: 'Documentation', icon: 'fa fa-fw fa-book', command: (event) =>{
              console.log(event);
            }},

            {label: 'Support', icon: 'fa fa-fw fa-support', command: (event) =>{
              console.log(event);
            }},
            {label: 'Social', icon: 'fa fa-fw fa-twitter', command: (event) =>{
              console.log(event);
            }}
        ];
    }

}
