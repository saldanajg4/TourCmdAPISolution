import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { ShowService } from '../../../../shows/shared/show.service';
import { ShowSingleComponent } from '../show-single/show-single.component';

@Component({
  selector: 'app-show-add',
  templateUrl: './show-add.component.html',
  styleUrls: ['./show-add.component.css']
})
export class ShowAddComponent implements OnInit, OnDestroy {
  private sub: Subscription;
  private tourId: string;
  public showCollectionForm: FormGroup;

  constructor(private route: ActivatedRoute, private showService: ShowService,
      private formBuilder: FormBuilder, private router: Router) { }
 

  ngOnInit(): void {
    //create a form group
    this.showCollectionForm = this.formBuilder.group({
      shows: this.formBuilder.array([])
    });

    this.addShow();

    this.sub = this.route.params.subscribe(
      params => {
        this.tourId = params['tourId'];
        console.log('tourId: ' + this.tourId);
      }
    );
  }

  //add one show
  addShow(){
    let showFormArray = this.showCollectionForm.get('shows') as FormArray;
    showFormArray.push(ShowSingleComponent.createShow());
  }
  removeShow(event){
    (this.showCollectionForm.get('shows')as FormArray).removeAt(event);
  }

  goBack(){
    this.router.navigateByUrl('/tour/'+this.tourId);
  }

  addShows(): void {
    if (this.showCollectionForm.dirty
      && this.showCollectionForm.value.shows.length) {

      let showCollection = automapper.map(
        'ShowCollectionFormModelShowsArray',
        'ShowCollectionForCreation',
        this.showCollectionForm.value.shows);

      this.showService.addShowCollection(this.tourId, showCollection)
        .subscribe(
          () => {
            this.router.navigateByUrl('/tour');
          });
    }
  }

  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }
}
