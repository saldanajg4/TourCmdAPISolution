import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { TourMasterDataService } from 'src/app/shared/tour-master-data.service';

@Component({
  selector: 'app-order-add',
  templateUrl: './order-add.component.html',
  styleUrls: ['./order-add.component.css']
})
export class OrderAddComponent implements OnInit {

  isAdmin = true;
  managers: any[];
  managerOptions: any[];
  orderForm: FormGroup;
  
  constructor(private masterDataSvc: TourMasterDataService, private formBuilder: FormBuilder) { }

  ngOnInit(): void {
    this.orderForm = this.formBuilder.group({
   
      manager: [''],
      customer: [''],
      description: [''],
      // startDate: [''],
      // endDate: [''],
      // shows: this.formBuilder.array([])
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

  addOrder(){}

}
