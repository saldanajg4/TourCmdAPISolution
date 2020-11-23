import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { Ingredient } from 'src/app/ingredient/ingredient.model';
import { IngredientService } from 'src/app/ingredient/ingredient.service';
import { ItemService } from 'src/app/items/shared/item.service';
import { ItemSingleComponent } from '../item-single/item-single.component';

@Component({
  selector: 'app-item-add',
  templateUrl: './item-add.component.html',
  styleUrls: ['./item-add.component.css']
})
export class ItemAddComponent implements OnInit {
  itemCollectionForm: FormGroup;
  isAdmin = true;
  ingredients: Ingredient[];

  constructor(private formBuilder: FormBuilder, private router: Router,
            private itemSvc: ItemService, private ingredientSvc: IngredientService) { }

  ngOnInit(): void {
    // this.getIngredients();
    this.itemCollectionForm = this.formBuilder.group(
      {
        items: this.formBuilder.array([])
      }
    );
    
    this.addItem();
  }


  addItem(){
    (this.itemCollectionForm.get('items') as FormArray).push(ItemSingleComponent.createItem());
  }
  removeItem(event){
    (this.itemCollectionForm.get('items') as FormArray).removeAt(event);
  }
  goBack(){
    this.router.navigateByUrl('/menu-items');
  }
  createItems(){
    if(this.itemCollectionForm.dirty
        && this.itemCollectionForm.value.items.length){
          let itemCollection = automapper.map(
            'ItemCollectionFormModelItemsArray',
            'ItemCollectionForCreation',
            this.itemCollectionForm.value.items);
          console.log('itemcollection');
          console.log(JSON.stringify(this.itemCollectionForm.value.items));
          this.itemSvc.createItemCollection(itemCollection)
            .subscribe(
              () => {
                this.router.navigateByUrl('/menu-items');
              }
            );
          
        }
  }

  // automapper.createMap('ItemCollectionFormModelItemsArray',
  //       'ItemCollectionForCreation');
  // addShows(): void {
  //   if (this.showCollectionForm.dirty
  //     && this.showCollectionForm.value.shows.length) {

  //     let showCollection = automapper.map(
  //       'ShowCollectionFormModelShowsArray',
  //       'ShowCollectionForCreation',
  //       this.showCollectionForm.value.shows);

  //     this.showService.addShowCollection(this.tourId, showCollection)
  //       .subscribe(
  //         () => {
  //           this.router.navigateByUrl('/tour');
  //         });
  //   }
  // }
}
