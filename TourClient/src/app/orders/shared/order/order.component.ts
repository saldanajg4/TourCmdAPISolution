import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Order } from '../order.model';
import { OrderService } from '../order.service';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.css']
})
export class OrderComponent implements OnInit {

  orders: Order[];
  constructor(private orderSvc: OrderService, private route: Router) { }

  ngOnInit(): void {
    this.orderSvc.getOrders().subscribe(
      data => this.orders = data,
      (err) => console.log(err),
      () => {
        console.log('loading orders complete');
        console.log(JSON.stringify(this.orders));
      }
    )
  }

  addOrderNavigate(){
    this.route.navigateByUrl('/order-add');
  }

}
