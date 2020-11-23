import { Component, Input, OnInit } from '@angular/core';
import { Item } from 'src/app/items/shared/item.model';

@Component({
  selector: 'app-items',
  templateUrl: './items.component.html',
  styleUrls: ['./items.component.css']
})
export class ItemsComponent implements OnInit {

  @Input()
  items: Item[];
  
  constructor() { }

  ngOnInit(): void {
  }

}
