import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Ingredient } from 'src/app/ingredient/ingredient.model';
import { IngredientService } from 'src/app/ingredient/ingredient.service';

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
  ingredients: Ingredient[];
  ingredientOptions:any[];
  
  constructor(private ingredientSvc: IngredientService) { 
    
  }

 //this will be called dataSvc.ingredientData.subscribe({do some stuff...})
  //dataSvc.updateIngredientData(newIngredients)
  ngOnInit(): void {
    this.getIngredients();
    
  }

  static createItem(){
    return new FormGroup(
      {
        itemName: new FormControl([]),
        ingredients: new FormControl([]),
        description: new FormControl([]),
        price: new FormControl([]),
        estimatedCost: new FormControl([])
      }
    )
  }
  getIngredients(){
    this.ingredientSvc.ingredientData.subscribe(
      (data) => {
        this.multiSelectIngredientFormat(data);
      }
    )
    // this.ingredientSvc.GetAllIngredients().subscribe(
    //   (data) => {
    //     this.ingredients = data;
    //     console.log(this.ingredients);
    //     this.multiSelectIngredientFormat();
    //   },
    //   (error) => console.log('Error getting ingredients ' + error),
    //   () => console.log('Finished getting ingredients')
    // )
  }

  multiSelectIngredientFormat(ingredientsSelected){
    this.ingredientOptions = ingredientsSelected.map(i => ({
      // label: `${i.IngredientName}`,
      label: i.ingredientName,
      value: {
              // IngredientId: i.ingredientId, //this will be added when I create many-to-many 
                                              //relationship
              IngredientName: i.ingredientName,
              IngredientCategoryId: i.ingredientCategoryId
             }
    }));
  }
  
}
