import { NgForm } from '@angular/forms';
import { PaymentDetail } from '../payment-details/shared/payment-detail.model';

export interface IAppState{
    paymentDetails: PaymentDetail[];
}