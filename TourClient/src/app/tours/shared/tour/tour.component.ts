import { Component, OnInit } from '@angular/core';
import { Tour } from '../tour.model';
import { TourService } from '../tour.service';

@Component({
  selector: 'app-tour',
  templateUrl: './tour.component.html',
  styleUrls: ['./tour.component.css']
})
export class TourComponent implements OnInit {
  tours: Tour[];

  constructor(private tourSvc: TourService) { }

  ngOnInit() {
    this.tourSvc.getTours().subscribe(
      ts => this.tours = ts,
      err => console.log("Error getting tours" + err),
      () => console.log(this.tours)
    )
  }

}
