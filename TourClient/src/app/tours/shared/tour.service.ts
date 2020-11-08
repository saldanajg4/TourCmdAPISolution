import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Tour } from './tour.model';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BaseService } from '../../shared/base.service'
import { TourWithEstimatedProfits } from './tour-with-estimated-profits.model';
import { TourWithShows } from './tour-with-shows.model';
import { TourWithEstimatedProfitsAndShows } from './tour-with-estimated-profits-and-shows.model';
import { TourForCreation } from './tour-for-creation.model';
import { TourWithManagerForCreation } from './tour-with-manager-for-creation.model';
import { TourWithShowsForCreation } from './tour-with-shows-for-creation.model';
import { TourWithManagerAndShowsForCreation } from './tour-with-manager-and-shows-for-creation.model';
import { TourData } from './tour-data.model';
@Injectable({
  providedIn: 'root'
})
export class TourService extends BaseService{

  token = '5ab36389-6610-459e-af42-0da7c3a68e8b';
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

  // headers = new HttpHeaders({
  //   'Cache-Control': 'no-cache, no-store, must-revalidate, post-check=0, pre-check=0',
  //   'Pragma': 'no-cache',
  //   'Expires': '0',
  //   'Access-Control-Allow-Origin' : '*',
  //   'Access-Control-Allow-Headers': 'Origin, X-Requested-With, Content-Type, Accept'
  // });

  

  getTours(): Observable<TourData>{
    console.log('getting all tours');
    // return this.http.get<Tour[]>(this.baseUrl+'/tour', {headers: this.headers, responseType: 'json'});
    return this.http.get<TourData>(this.baseUrl+'/tour',
    {headers: {'Authorization': this.token}});
  }

  getTour(tourId: string): Observable<Tour>{
    // return this.http.get<Tour[]>(this.baseUrl+'/tour', {headers: this.headers, responseType: 'json'});
    return this.http.get<Tour>(this.baseUrl+'/tour/'+tourId,
    { headers: {'Accept': 'application/vnd.jose.tour+json', 'Authorization': this.token}});
  }

  getTourWithEstimatedProfits(tourId: string): Observable<TourWithEstimatedProfits>{
    // return this.http.get<Tour[]>(this.baseUrl+'/tour', {headers: this.headers, responseType: 'json'});
    return this.http.get<TourWithEstimatedProfits>(`${this.baseUrl}/tour/${tourId}`,
    { headers: {'Accept': 'application/vnd.jose.tourwithestimatedprofits+json',
                'Authorization': this.token}});
  }

  getTourWithShows(tourId: string): Observable<TourWithShows>{
    // return this.http.get<Tour[]>(this.baseUrl+'/tour', {headers: this.headers, responseType: 'json'});
    return this.http.get<TourWithShows>(this.baseUrl+'/tour/'+tourId,
    { headers: {'Accept': 'application/vnd.jose.tourwithshows+json',
                'Authorization': this.token}});
  }

  getTourWithEstimatedProfitsAndShows(tourId: string): Observable<TourWithEstimatedProfitsAndShows>{
    // return this.http.get<Tour[]>(this.baseUrl+'/tour', {headers: this.headers, responseType: 'json'});
    return this.http.get<TourWithEstimatedProfitsAndShows>(`${this.baseUrl}/tour/${tourId}`,
    { headers: {'Accept': 'application/vnd.jose.tourwithestimatedprofitsandshows+json',
                'Authorization': this.token}});
  }

  addTour(tourToAdd: TourForCreation): Observable<Tour>{
    console.log('adding tour wo manager');
    return this.http.post<Tour>(`${this.baseUrl}/tour`, tourToAdd,
      {headers: {'Content-Type': 'application/vnd.jose.tourforcreation+json',
                  'Authorization': this.token}});
  }

  addTourWithManager(tourToAdd: TourWithManagerForCreation): Observable<Tour>{
    console.log("creating tour with manager but no shows");
    return this.http.post<Tour>(`${this.baseUrl}/tour`, tourToAdd,
      {headers: {'Content-Type': 'application/vnd.jose.tourwithmanagerforcreation+json',
      'Authorization': this.token}});
  }

  addTourWithShows(tourToAdd: TourWithShowsForCreation): Observable<Tour>{
    console.log('adding tour wo manager');
    return this.http.post<Tour>(`${this.baseUrl}/tour`, tourToAdd,
      {headers: {'Content-Type': 'application/vnd.jose.tourwithshowsforcreation+json',
      'Authorization': this.token}});
  }

  addTourWithManagerAndShows(tourToAdd: TourWithManagerAndShowsForCreation): Observable<Tour>{
    return this.http.post<Tour>(`${this.baseUrl}/tour`, tourToAdd,
      {headers: {'Content-Type': 'application/vnd.jose.tourwithmanagerandshowsforcreation+json',
      'Authorization': this.token}});
  }



}
