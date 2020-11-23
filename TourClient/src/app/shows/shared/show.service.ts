import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseService } from 'src/app/shared/base.service';
import { ShowForCreation } from '../show-for-creation.model';
import { Show } from './show.model';

@Injectable({
  providedIn: 'root'
})
export class ShowService extends BaseService {

  constructor(private http: HttpClient) {
    super();
  }

  addShowCollection(tourId: string, showsToAdd: ShowForCreation[]):Observable<Show[]>{
    return this.http.post<Show[]>(`${this.baseUrl}/tour/${tourId}/showcollection`, showsToAdd,
      {headers: {'Content-type': 'application/vnd.jose.showcollectionforcreation+json'}});
  }
}
