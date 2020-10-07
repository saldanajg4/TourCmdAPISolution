import { Component, Input, OnInit } from '@angular/core';
import { Show } from 'src/app/shows/shared/show.model';

@Component({
  selector: 'app-shows',
  templateUrl: './shows.component.html',
  styleUrls: ['./shows.component.css']
})
export class ShowsComponent implements OnInit {

  @Input()
  shows: Show[];

  constructor() { }

  ngOnInit(): void {
  }

}