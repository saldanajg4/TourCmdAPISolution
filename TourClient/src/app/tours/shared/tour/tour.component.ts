import { Component, OnInit } from '@angular/core';
import { Tour } from '../tour.model';
import { TourService } from '../tour.service';
import {TableModule} from 'primeng/table';
import { stringify } from 'querystring';
import {DialogModule} from 'primeng/dialog';
import {ButtonModule} from 'primeng/button';
import { Router } from '@angular/router';

@Component({
  selector: 'app-tour',
  templateUrl: './tour.component.html',
  styleUrls: ['./tour.component.css']
})
export class TourComponent implements OnInit {
  tours: Tour[];
  tour: any;
  displayTour = false;

  constructor(private tourSvc: TourService, private route: Router) { }

  ngOnInit() {
    this.tourSvc.getTours().subscribe(
      ts => this.tours = ts,
      err => console.log(JSON.stringify(err)),
      () => {
        console.log(this.tours);
      }
    );
    
  }
  showDialog(tourId: string){
    this.displayTour = true;
    console.log("tourId: " + tourId);
  }
  addTourNavigate(){
    this.route.navigateByUrl('/tour-add');
  }
  closeDialog(){
    this.displayTour = false;
  }
}
