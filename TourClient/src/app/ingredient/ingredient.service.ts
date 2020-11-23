import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BaseService } from '../shared/base.service';
import { Ingredient } from './ingredient.model';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class IngredientService extends BaseService{

  private ingredientSource = new BehaviorSubject<Ingredient[]>([]);
  ingredientData = this.ingredientSource.asObservable();

  constructor(private http: HttpClient) {
    super();
  }

  //this will be called dataSvc.ingredientData.subscribe({do some stuff...})
  //dataSvc.updateIngredientData(newIngredients)
  updateIngredientData(ingredientData: Ingredient[]){
    this.ingredientSource.next(ingredientData);
  }


  public GetAllIngredients(): Observable<Ingredient[]>{
    return this.http.get<Ingredient[]>(`${this.baseUrl}/ingredientcategory/alling`,
      {headers: {"Accept": "application/vnd.jose.allingredients+json"}});
  }

  // getOrderWithItems(orderId: number): Observable<OrderWithItems[]>{
  //   return this.http.get<OrderWithItems[]>(`${this.baseUrl}/order/${orderId}`,
  //     {headers: {'Accept': 'application/vnd.jose.orderwithitems+json'}});
  // }
}
