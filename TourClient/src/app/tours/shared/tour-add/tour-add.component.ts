import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { Band } from 'src/app/shared/band.model';
import { Manager } from 'src/app/shared/manager.model';
import { TourMasterDataService } from 'src/app/shared/tour-master-data.service';
import { ShowSingleComponent } from '../shows/show-single/show-single.component';
import { TourService } from '../tour.service';



@Component({
  selector: 'app-tour-add',
  templateUrl: './tour-add.component.html',
  styleUrls: ['./tour-add.component.css']
})
/**
 * Tour add component contains a child component show single form array that
 * sends 2 input and 1 output parameters to show single component
 */
export class TourAddComponent implements OnInit {
  public tourForm: FormGroup;
  bands: Band[] = [];
  bandOptions: any[];
  managerOptions: any[];
  selectedBand: Band;
  isAdmin = false;

  managers: Manager[];
  constructor(private masterDataSvc: TourMasterDataService,
    private tourSvc: TourService,
    private formBuilder: FormBuilder,
    private router: Router) { }

  ngOnInit(): void {
    this.tourForm = this.formBuilder.group({
      band: [''],
      manager: [''],
      title: [''],
      description: [''],
      startDate: [''],
      endDate: [''],
      shows: this.formBuilder.array([])
    });
    //get the bands from the tour master data service
    this.masterDataSvc.getBands().subscribe(
      bands => {
        this.bands = bands;
        this.bandOptions = this.bands.map(band => ({
          // ...band,
          label: `${band.name}`,
          value: `${band.bandId}`
          // name: `${band.name}`,
          // code: `${band.bandId}`
          // {label:'Select City', value:null},
        }))
      });

    if (this.isAdmin) {
      this.masterDataSvc.getManagers().subscribe(
        managers => {
          this.managers = managers;
          this.managerOptions = this.managers.map(m => ({
            label: `${m.name}`,
            value: `${m.managerId}`
          }))
          console.log(JSON.stringify(this.managers));
        }
      );
    }

  }

  addShow(){
    let showsFormArray = this.tourForm.get('shows') as FormArray;
    //add show
    showsFormArray.push(ShowSingleComponent.createShow());

  }
  removeShow(event){
    (this.tourForm.get('shows')as FormArray).removeAt(event);
  }
  
  addTour() {
    console.log(JSON.stringify(this.tourForm.value));
    if (this.tourForm.dirty) {
      if (this.isAdmin === true) {
        if(this.tourForm.value.shows.length){
          let tour = automapper.map(
            'TourFormModel',
            'TourWithManagerAndShowsForCreation',
            this.tourForm.value);

            this.tourSvc.addTourWithManagerAndShows(tour).subscribe(
              () => {
                this.router.navigateByUrl('/tour');
              })
        }
        else{
          let tour = automapper.map(
            'TourFormModel',
            'TourWithManagerForCreation',
            this.tourForm.value);
  
          this.tourSvc.addTourWithManager(tour).subscribe(
            () => {
              this.router.navigateByUrl('/tour');
            })
        }
        
      }//end if isAdmin
      else {
        if(this.tourForm.value.shows.length){
          let tour = automapper.map(
            'TourFormModel',
            'TourWithShowsForCreation',
            this.tourForm.value);

          this.tourSvc.addTourWithShows(tour).subscribe(
            () => {
              this.router.navigateByUrl('/tour');
            })
        }
        else {
          console.log("not a manager adding a tour.")
          let tour = automapper.map(
            'TourFormModel',
            'TourForCreation',
            this.tourForm.value);

          this.tourSvc.addTour(tour).subscribe(
            () => {
              this.router.navigateByUrl('/tour');
            })
        }
      }
    }//end if form dirty
   
  }

}
