import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { from, Subscription } from 'rxjs';
import { map, reduce, scan } from 'rxjs/operators';
import { OrderService } from '../order.service';

@Component({
  selector: 'app-order-detail',
  templateUrl: './order-detail.component.html',
  styleUrls: ['./order-detail.component.css']
})
export class OrderDetailComponent implements OnInit, OnDestroy {
  orderId: number;
  order: any;
  sub: Subscription;
  isAdmin = true;
  total: number;
  constructor(private route: ActivatedRoute, private orderSvc: OrderService) { }
  

  ngOnInit(): void {
    this.sub = this.route.params.subscribe(
      params => {
        this.orderId = params["orderId"];
        if(this.isAdmin){
          this.orderSvc.getOrderWithItems(this.orderId).subscribe(
            data => {
              this.order = data;
              this.calculateTotal();
            },
            (err) => console.log(err),
            () => console.log(JSON.stringify(this.order))
          )
        }
      }
    )
  }
  calculateTotal(){
    // let total = (tot:number, curr:number) => tot + curr;
    this.sub = from(this.order.items.map(item => item.price)).pipe(
      reduce((acc:number, val:number) => acc + val)
    ).subscribe( val => this.total = val)
 
  }

  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }

}
