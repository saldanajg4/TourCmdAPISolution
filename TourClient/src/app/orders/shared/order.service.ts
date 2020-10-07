import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseService } from 'src/app/shared/base.service';
import { OrderWithItems } from './order-with-items.model';
import { Order } from './order.model';

@Injectable()
export class OrderService extends BaseService{

  constructor(private http: HttpClient) {
    super();
  }

  getOrders(): Observable<Order[]>{
    return this.http.get<Order[]>(this.baseUrl + '/order');
  }
  getOrderWithItems(orderId: number): Observable<OrderWithItems[]>{
    return this.http.get<OrderWithItems[]>(`${this.baseUrl}/order/${orderId}`,
      {headers: {'Accept': 'application/vnd.jose.orderwithitems+json'}});
  }
  // getTourWithEstimatedProfits(tourId: string): Observable<TourWithEstimatedProfits>{
  //   // return this.http.get<Tour[]>(this.baseUrl+'/tour', {headers: this.headers, responseType: 'json'});
  //   return this.http.get<TourWithEstimatedProfits>(`${this.baseUrl}/tour/${tourId}`,
  //   { headers: {'Accept': 'application/vnd.jose.tourwithestimatedprofits+json'}});
  // }
}
