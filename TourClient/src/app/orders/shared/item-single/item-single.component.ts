import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-item-single',
  templateUrl: './item-single.component.html',
  styleUrls: ['./item-single.component.css']
})
export class ItemSingleComponent implements OnInit {
  @Input() itemIndex: number;
  @Input() item: FormGroup;
  @Input() isAdmin: boolean;
  @Output() removeItemClicked: EventEmitter<number> = new EventEmitter<number>();
  
  constructor() { }

  ngOnInit(): void {
  }

  static createItem(){
    return new FormGroup(
      {
        itemName: new FormControl([]),
        description: new FormControl([]),
        price: new FormControl([]),
        estimatedCost: new FormControl([])
      }
    )
  }

}
