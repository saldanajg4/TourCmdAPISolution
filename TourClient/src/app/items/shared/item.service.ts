import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseService } from 'src/app/shared/base.service';
import { ItemForCreation } from './item-for-creation.model';
import { ItemWithEstimatedCost } from './item-with-estimated-cost.model';
import { Item } from './item.model';

@Injectable({
  providedIn: 'root'
})
export class ItemService extends BaseService{

  constructor(private http: HttpClient) {
    super();
  }

  addItemWithEstimatedCost(itemToAdd: ItemWithEstimatedCost): Observable<ItemWithEstimatedCost>{
    return this.http.post<ItemWithEstimatedCost>(`${this.baseUrl}/item`, itemToAdd,
      {headers: {'Content-Type': 'application/vnd.jose.itemwithestimatedcost+json'}});
  }
  
  createItemCollection(itemsCollection: ItemForCreation[]): Observable<Item[]>{
    console.log(JSON.stringify(itemsCollection));
    return this.http.post<Item[]>(`${this.baseUrl}/itemcollection`, itemsCollection,
      {headers: {'Content-Type': 'application/vnd.jose.itemcollectionforcreation+json'}});
  }


  // addShowCollection(tourId: string, showsToAdd: ShowForCreation[]):Observable<Show[]>{
  //   return this.http.post<Show[]>(`${this.baseUrl}/tour/${tourId}/showcollection`, showsToAdd,
  //     {headers: {'Content-type': 'application/vnd.jose.showcollectionforcreation+json'}});
  // }

  // addTour(tourToAdd: TourForCreation): Observable<Tour>{
  //   console.log('adding tour wo manager');
  //   return this.http.post<Tour>(`${this.baseUrl}/tour`, tourToAdd,
  //     {headers: {'Content-Type': 'application/vnd.jose.tourforcreation+json'}});
  // }

  // addTourWithManager(tourToAdd: TourWithManagerForCreation): Observable<Tour>{
  //   return this.http.post<Tour>(`${this.baseUrl}/tour`, tourToAdd,
  //     {headers: {'Content-Type': 'application/vnd.jose.tourwithmanagerforcreation+json'}});
  // }


}
