import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Show } from 'src/app/shows/shared/show.model';

@Component({
  selector: 'app-shows',
  templateUrl: './shows.component.html',
  styleUrls: ['./shows.component.css']
})
export class ShowsComponent implements OnInit {

  @Input()
  shows: Show[];

  @Input()
  tourId: string;

  constructor(private router: Router) { }

  ngOnInit(): void {
  }
  addShowNavigate(tourId){
    this.router.navigateByUrl('/tour/'+tourId+'/show-add');
  }
}
