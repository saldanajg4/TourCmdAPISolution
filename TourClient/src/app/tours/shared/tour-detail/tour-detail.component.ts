import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TourService } from '../tour.service';
import {CardModule} from 'primeng/card';
import { Show } from 'src/app/shows/shared/show.model';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-tour-detail',
  templateUrl: './tour-detail.component.html',
  styleUrls: ['./tour-detail.component.css']
})
export class TourDetailComponent implements OnInit, OnDestroy {

  tourId: string;
  tour: any;
  sub: Subscription;
  isAdmin = false;
  shows: Show[];

  constructor(private route: ActivatedRoute, private tourSvc: TourService) { }
  

  ngOnInit(): void {
    this.sub = this.route.params.subscribe(
      params => {
        this.tourId = params["tourId"];
        if(this.isAdmin === true){
          this.tourSvc.getTourWithEstimatedProfitsAndShows(this.tourId).subscribe(
            t => {
              this.tour = t;
              this.shows = t.shows;
              console.log(this.tour);
            }
          )
        }
        else{
          this.tourSvc.getTourWithShows(this.tourId).subscribe(
            t => {
              this.tour = t;
              this.shows = t.shows;
              console.log(this.tour);
            })
        }
      })
  }
  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }
}
