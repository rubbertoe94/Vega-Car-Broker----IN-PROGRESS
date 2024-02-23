import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { MakeViewModel } from '../models/MakeViewModel';


@Injectable({
  providedIn: 'root'
})
export class VehicleService {
baseUrl: string = "https://localhost:7220/api";

  constructor(private http: HttpClient) { }


getMakes(): Observable<any> {
  let endpointUrl = this.baseUrl + '/makes';
  return this.http.get<any>(endpointUrl);
}

getFeatures(): Observable<any> {
  let endpointUrl = this.baseUrl + '/vehicles/features';
  return this.http.get<any>(endpointUrl);
}

}
