import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { PaymentDetail } from '../shared/payment-detail.model';
import { PaymentDetailService } from '../shared/payment-detail.service';

@Component({
  selector: 'app-payment-detail-list',
  templateUrl: './payment-detail-list.component.html',
  styleUrls: ['./payment-detail-list.component.css']
})
export class PaymentDetailListComponent implements OnInit {
  pdList: PaymentDetail[];

  constructor(public pdService: PaymentDetailService, private toastSvc: ToastrService) { }

  //this.pdService.paymentDetailsList gets updated every time the list is updated.
  //BehaviorSubject used as observables
  ngOnInit(): void {
    this.pdService.getPaymentDetails().subscribe(
      (data) => {
        this.pdService.updatePdDataSources(data);
        this.pdService.pdData.subscribe(data => {
          this.pdService.paymentDetailsList = data;
        })
      }
    )
  }

  onDelete(id:number){
    if (confirm('Are you sure to delete this record ?')) {
    this.pdService.deletePaymentDetails(id).subscribe(
      (data) => {
        this.toastSvc.warning('Record Deleted.');
        this.pdService.getPaymentDetails().subscribe(data => this.pdService.paymentDetailsList = data);
      },
      (err) => this.toastSvc.error('Error deleting record. ' + err)
    )
    }
  }
  populateForm(pd: PaymentDetail){
    this.pdService.formData = Object.assign({}, pd);
    // this.pdService.updatePdDataSource(this.pdService.formData);
    // console.log('thisformdata: ' );
    // console.log(this.pdService.formData);

    // console.log('pdToUpdate as observable: ' );
    // this.pdService.pdToUpdate.subscribe(data => console.log(data));
  }

}
