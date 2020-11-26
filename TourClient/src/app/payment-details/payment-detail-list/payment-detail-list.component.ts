import { Component, OnInit } from '@angular/core';
import { NgRedux,select } from 'ng2-redux';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { IAppState } from 'src/app/store/IAppState';
import { PaymentDetailActions } from '../shared/payment-detail.actions';
import { PaymentDetail } from '../shared/payment-detail.model';
import { PaymentDetailService } from '../shared/payment-detail.service';

@Component({
  selector: 'app-payment-detail-list',
  templateUrl: './payment-detail-list.component.html',
  styleUrls: ['./payment-detail-list.component.css']
})
export class PaymentDetailListComponent implements OnInit {
  // pdList: PaymentDetail[];
  @select('paymentDetails') paymentDetails$: Observable<PaymentDetail>

  // constructor(public pdService: PaymentDetailService, private toastSvc: ToastrService) { }
  constructor(private toastSvc: ToastrService, 
    private ngRedux: NgRedux<IAppState>,
    private paymentActions: PaymentDetailActions,
    private pdService: PaymentDetailService) { }

  //this.pdService.paymentDetailsList gets updated every time the list is updated.
  //BehaviorSubject used as observables
  ngOnInit(): void {
    // this.pdService.getPaymentDetails().subscribe(
    //   (data) => {
    //     this.pdService.updatePdDataSources(data);
    //     this.pdService.pdData.subscribe(data => {
    //       this.pdService.paymentDetailsList = data;
    //     })
    //   }
    // )
    this.paymentActions.getPaymentDetails();
    // componentHandler.upgradeDom();
  }

  onDelete(id:number){
    if (confirm('Are you sure to delete this record ?')) {
      this.paymentActions.deletePaymentDetail(id);
    // this.pdService.deletePaymentDetails(id).subscribe(
    //   (data) => {
    //     this.toastSvc.warning('Record Deleted.');
    //     this.pdService.getPaymentDetails().subscribe(data => this.pdService.paymentDetailsList = data);
    //   },
    //   (err) => this.toastSvc.error('Error deleting record. ' + err)
    // )
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
