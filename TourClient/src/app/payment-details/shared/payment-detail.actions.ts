import { Injectable } from '@angular/core';
import { NgRedux } from 'ng2-redux';
import { IAppState } from 'src/app/store/IAppState';
import { PaymentDetailService } from './payment-detail.service';

export const PAYMENT_DETAIL_REQUEST = 'pay-details/PAYMENT_DETAIL_REQUEST';

@Injectable()
export class PaymentDetailActions{
    constructor(private ngRedux: NgRedux<IAppState>,
        private payService: PaymentDetailService){}
    
    getPaymentDetails(){
        this.payService.getPaymentDetails().subscribe(
            paymentDetails => {
                this.ngRedux.dispatch({
                    type: PAYMENT_DETAIL_REQUEST,
                    paymentDetails,
                });
            });
    }

}