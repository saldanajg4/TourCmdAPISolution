import { Component, OnInit } from '@angular/core';
import { Tour } from '../tour.model';
import { TourService } from '../tour.service';
import {TableModule} from 'primeng/table';

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
      err => console.log(JSON.stringify(err)),
      () => console.log(this.tours)
    )
  }

}
