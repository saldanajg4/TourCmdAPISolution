import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { PaymentDetailService } from '../shared/payment-detail.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-payment-detail',
  templateUrl: './payment-detail.component.html',
  styleUrls: ['./payment-detail.component.css']
})
export class PaymentDetailComponent implements OnInit {

  constructor(public service: PaymentDetailService, private toastSvc: ToastrService) { }

  ngOnInit(): void {
  }
  //the form value contains the PaymentDetail object model to be sent to webapi
  onSubmit(form: NgForm) {
    console.log('the form');
    console.log(this.service.formData.id);
    if(this.service.formData.id > 0){
      this.patch(form);
    } 
    else  {
      this.insert(form);
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
  private insert(form: NgForm) {
    
    this.service.postPaymentDetails(form.value).subscribe(
      (data) => {
        this.toastSvc.success('card added.');
        this.resetForm(form);
        this.service.getPaymentDetails().subscribe(
          (data) => {
            this.service.updatePdDataSources(data);
          }
        );
      },
      (err) => this.toastSvc.error('Error adding card.')
    );
  }

  private patch(form: NgForm) {
    this.service.patchPaymentDetails().subscribe(
      (data) => {
        this.toastSvc.success('card updated.');
        this.resetForm(form);
        this.service.getPaymentDetails().subscribe(
          (data) => {
            this.service.updatePdDataSources(data);
          }
        );
      },
      (err) => this.toastSvc.error('Error updating card.')
    );
  }
}
