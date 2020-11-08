import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { BaseService } from 'src/app/shared/base.service';
import { PaymentDetail } from './payment-detail.model';

@Injectable({
  providedIn: 'root'
})
export class PaymentDetailService extends BaseService{
  formData: PaymentDetail= {
    cvv: null,
    cardNumber: null,
    cardOwnerName: null,
    expirationDate: null,
    id: null
  };
  //this will be used in form to display list
  paymentDetailsList: PaymentDetail[] = [];
  constructor(private http: HttpClient) {
    super();
  }

  //this will update PaymentDetails requested
  private pdList = new BehaviorSubject<PaymentDetail[]>([]);
  pdData = this.pdList.asObservable();//I will need to subscribe to this 
                                        //observable
  //method called to update new observed data
  updatePdDataSources(newPdData: PaymentDetail[]){
    this.pdList.next(newPdData);
  }

  //this will update One PaymentDetail to be updated
  private pdDataSourceToUpdate = new BehaviorSubject<PaymentDetail>(null);
  pdToUpdate = this.pdDataSourceToUpdate.asObservable();//I will need to subscribe to this 
                                        //observable
  //method called to update new observed data
  updatePdDataSource(newPdData: PaymentDetail){
    this.pdDataSourceToUpdate.next(newPdData);
  }

  postPaymentDetails(payment: PaymentDetail): Observable<PaymentDetail>{
    return this.http.post<PaymentDetail>(`${this.baseUrl}/payment`, payment,
      {headers: {'Content-Type': 'application/vnd.jose.paymentdetailsforcreation+json'}});
  }

  getPaymentDetails(): Observable<PaymentDetail[]>{
    return this.http.get<PaymentDetail[]>(`${this.baseUrl}/payment`,
    {headers: {'Accept': 'application/vnd.jose.paymentdetails+json'}});
  }

  deletePaymentDetails(id: number): Observable<PaymentDetail>{
    return this.http.delete<PaymentDetail>(`${this.baseUrl}/payment/${id}`,
    {headers: {'Accept': 'application/json'}});
  }

  patchPaymentDetails(): Observable<PaymentDetail>{
    return this.http.patch<PaymentDetail>(`${this.baseUrl}/payment/${this.formData.id}`,
    this.formData);
  }

  // addTour(tourToAdd: TourForCreation): Observable<Tour>{
  //   console.log('adding tour wo manager');
  //   return this.http.post<Tour>(`${this.baseUrl}/tour`, tourToAdd,
  //     {headers: {'Content-Type': 'application/vnd.jose.tourforcreation+json',
  //                 'Authorization': this.token}});
  // }
}
