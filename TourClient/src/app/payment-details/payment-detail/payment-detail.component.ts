import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { PaymentDetailService } from '../shared/payment-detail.service';
import { ToastrService } from 'ngx-toastr';
import { NgRedux } from 'ng2-redux';
import { IAppState } from 'src/app/store/IAppState';
import { Observable } from 'rxjs';
import { PaymentDetail } from '../shared/payment-detail.model';
import { PaymentDetailActions } from '../shared/payment-detail.actions';

@Component({
  selector: 'app-payment-detail',
  templateUrl: './payment-detail.component.html',
  styleUrls: ['./payment-detail.component.css']
})
export class PaymentDetailComponent implements OnInit {

  constructor(public service: PaymentDetailService, private toastSvc: ToastrService,
      private ngRedux: NgRedux<IAppState>,
      private paymentActions: PaymentDetailActions) { }
    

  ngOnInit(): void {
  }
  //the form value contains the PaymentDetail object model to be sent to webapi
  onSubmit(form: NgForm) {
    if(this.service.formData.id > 0){
      // this.patch(form);
      this.paymentActions.updatePaymentDetail(this.service.formData.id);

    } 
    else  {
      // this.insert(form);
      
      this.paymentActions.insertPaymentDetail(form);
    }
  }
  resetForm(form?: NgForm) {
    if (form != null)
      form.form.reset();
    this.service.formData = {
      id: 0,
      cardOwnerName: '',
      cardNumber: '',
      expirationDate: '',
      cvv: ''
    }
  }

}
