import { Component, OnInit } from '@angular/core';
import { Ingredient } from './ingredient.model';
import { IngredientService } from './ingredient.service';

@Component({
  selector: 'app-ingredient',
  templateUrl: './ingredient.component.html',
  styleUrls: ['./ingredient.component.css']
})
export class IngredientComponent implements OnInit {
ingredients: Ingredient[];

  constructor(private ingredientSvc: IngredientService) { }

  ngOnInit(): void {
    this.ingredientSvc.GetAllIngredients().subscribe(
      (data) => {
        this.ingredients = data;
        console.log(JSON.stringify(this.ingredients));
      },
      (err) => console.log('Error getting ingredients'),
      () => console.log('finish getting ingredients')
    )
  }

  addIngredient(){
    
  }

}
