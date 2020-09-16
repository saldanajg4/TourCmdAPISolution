import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Tour } from './tour.model';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BaseService } from '../../../shared/base.service'
@Injectable({
  providedIn: 'root'
})
export class TourService extends BaseService{

  constructor(private http: HttpClient) {
    super();
   }
  //   headerDict = {
  //   'Content-Type': 'application/json',
  //   'Accept': 'application/json',
  //   'Access-Control-Allow-Headers': 'Content-Type',
  //   'Access-Control-Allow-Origin' : '*'
  // }
  
  //  requestOptions = {                                                                                                                                                                                 
  //   headers: new Headers(this.headerDict), 
  // };

  headers = new HttpHeaders({
    'Cache-Control': 'no-cache, no-store, must-revalidate, post-check=0, pre-check=0',
    'Pragma': 'no-cache',
    'Expires': '0',
    'Access-Control-Allow-Origin' : '*',
    'Access-Control-Allow-Headers': 'Origin, X-Requested-With, Content-Type, Accept'
  });

  

  getTours(): Observable<Tour[]>{
    // return this.http.get<Tour[]>(this.baseUrl+'/tour', {headers: this.headers, responseType: 'json'});
    return this.http.get<Tour[]>(this.baseUrl+'/tour');
  }
}
