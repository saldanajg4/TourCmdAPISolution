import { Component, OnInit } from '@angular/core';
import { NgRedux,select } from 'ng2-redux';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { AppUserAuth } from 'src/app/security/app-user-auth';
import { SecurityService } from 'src/app/security/security.service';
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
  //subscribing to the state paymentDetails property from the store
  securityObject: AppUserAuth = null;
  @select('paymentDetails') paymentDetails$: Observable<PaymentDetail>

  // constructor(public pdService: PaymentDetailService, private toastSvc: ToastrService) { }
  constructor(private toastSvc: ToastrService, 
    private ngRedux: NgRedux<IAppState>,
    private paymentActions: PaymentDetailActions,
    private pdService: PaymentDetailService,
    private securityService: SecurityService) { 
      this.securityObject = this.securityService.securityObject;
    }

  ngOnInit(): void {
    this.paymentActions.getPaymentDetails();
  }

  onDelete(id:number){
    if (confirm('Are you sure to delete this record ?')) {
      this.paymentActions.deletePaymentDetail(id);
    }
  }
  populateForm(pd: PaymentDetail){
    this.pdService.formData = Object.assign({}, pd);
  }

}
