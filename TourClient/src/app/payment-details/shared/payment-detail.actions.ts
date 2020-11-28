import { Injectable } from '@angular/core';
import { NgForm } from '@angular/forms';
import { NgRedux } from 'ng2-redux';
import { ToastrService } from 'ngx-toastr';
import { IAppState } from 'src/app/store/IAppState';
import { PaymentDetail } from './payment-detail.model';
import { PaymentDetailService } from './payment-detail.service';

export const PAYMENT_DETAIL_REQUEST = 'pay-details/PAYMENT_DETAIL_REQUEST';

@Injectable()
export class PaymentDetailActions {
    constructor(private ngRedux: NgRedux<IAppState>,
        private payService: PaymentDetailService,
        private toastrSvc: ToastrService) { }

    getPaymentDetails() {
        this.payService.getPaymentDetails().subscribe(
            paymentDetails => {
                this.ngRedux.dispatch({
                    type: PAYMENT_DETAIL_REQUEST,
                    paymentDetails,
                });
            },
            (err) => this.toastrSvc.error('Error inserting payment details')
        );
    }
    insertPaymentDetail(payment: NgForm) {
        this.payService.postPaymentDetails(payment.value).subscribe(
            payDetailInserted => {
                this.getPaymentDetails();
                this.toastrSvc.success('card added.');
                this.payService.resetForm(payment);
            },
            (err) => this.toastrSvc.error('Error inserting payment details')
        )
    }

    updatePaymentDetail(paymentDetailId: number) {
        this.payService.patchPaymentDetails().subscribe(
            updatedPayment => {
                this.getPaymentDetails();
                this.toastrSvc.info('Payment details updated.');
            },
            (err) => this.toastrSvc.error('Error updating record.')  
        )
    }
    deletePaymentDetail(paymentDetailId: number) {
        this.payService.deletePaymentDetails(paymentDetailId).subscribe(
            (deletedPayment) => {
                this.getPaymentDetails();
                this.toastrSvc.warning('Payment details deleted.');
            },
            (err) => this.toastrSvc.error('Error deleting record.')
        )
    }
}