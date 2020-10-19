import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Item } from 'src/app/items/shared/item.model';
import { OrderService } from '../order.service';

@Component({
  selector: 'app-menu-items',
  templateUrl: './menu-items.component.html',
  styleUrls: ['./menu-items.component.css']
})
export class MenuItemsComponent implements OnInit {

  items: Item[];
  
  constructor(private orderSvc: OrderService, private router: Router) { }

  ngOnInit(): void {
    this.orderSvc.getItems().subscribe(
      (data =>{
        this.items = data;
      })
    )
  }
  addItemNavigate(){
    this.router.navigateByUrl('/item-add');
  }

}
