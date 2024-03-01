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

create(vehicle: any): Observable<any> {
  let endpointUrl = this.baseUrl + '/vehicles';
  return this.http.post<any>(endpointUrl, vehicle);
}

getVehicle(id: number): Observable<any> {
  let endpointUrl = this.baseUrl + '/vehicles/' + id;
  return this.http.get<any>(endpointUrl);
}

getVehicles(): Observable<any> {
  let endpointUrl = this.baseUrl + '/vehicles/allVehicles';
  return this.http.get<any>(endpointUrl);
}

}
