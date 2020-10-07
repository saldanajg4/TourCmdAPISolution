import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseService } from 'src/app/shared/base.service';
import { Band } from './band.model';
import { Manager } from './manager.model';

@Injectable({
  providedIn: 'root'
})
export class TourMasterDataService extends BaseService {

  constructor(private http: HttpClient) {
    super();
  }

  getBands(): Observable<Band[]>{
    return this.http.get<Band[]>(`${this.baseUrl}/band`);
  }

  getManagers(): Observable<Manager[]>{
    return this.http.get<Manager[]>(`${this.baseUrl}/manager`);
  }

}
