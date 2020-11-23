import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Ingredient } from 'src/app/ingredient/ingredient.model';
import { IngredientService } from 'src/app/ingredient/ingredient.service';
import { Item } from 'src/app/items/shared/item.model';
import { OrderService } from '../order.service';

@Component({
  selector: 'app-menu-items',
  templateUrl: './menu-items.component.html',
  styleUrls: ['./menu-items.component.css']
})
export class MenuItemsComponent implements OnInit {

  items: Item[];
  ingredients: Ingredient[];
  
  constructor(private orderSvc: OrderService,
              private ingredientSvc: IngredientService, private router: Router) { }
  //this will be called dataSvc.ingredientData.subscribe({do some stuff...})
  //dataSvc.updateIngredientData(newIngredients)

  ngOnInit(): void {
    this.orderSvc.getItems().subscribe(
      (data =>{
        this.items = data;
        this.populateIngredients();
      })
    )
  }
  addItemNavigate(){
    this.router.navigateByUrl('/item-add');
  }
  //the observable BehaviourSubject is updated when items page is selected
  populateIngredients(){
    this.ingredientSvc.GetAllIngredients().subscribe(
      (data) => {
        this.ingredients = data;
        this.ingredientSvc.updateIngredientData(this.ingredients);
      }
    )
  }

}
