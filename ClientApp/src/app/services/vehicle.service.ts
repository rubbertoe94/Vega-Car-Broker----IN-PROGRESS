import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { MakeViewModel } from '../models/MakeViewModel';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';



@Injectable({
  providedIn: 'root'
})
export class VehicleService {
private readonly baseUrl: string = "https://localhost:7220/api";
private readonly vehicleEndpoint: string = this.baseUrl + "/vehicles";
private readonly getVehiclesEndpoint: string = this.vehicleEndpoint + "/allVehicles";

  constructor(private http: HttpClient) { }


getMakes(): Observable<any> {
  let endpointUrl = this.baseUrl + '/makes';
  return this.http.get<any>(endpointUrl);
}

getFeatures(): Observable<any> {
  let endpointUrl = this.baseUrl + '/vehicles/features';
  return this.http.get<any>(endpointUrl);
}

create(newVehicle: any): Observable<any> {
  return this.http.post<any>(this.vehicleEndpoint, newVehicle);
}

getVehicle(id: number): Observable<any> {
  let endpointUrl = this.baseUrl + '/vehicles/' + id;
  return this.http.get<any>(endpointUrl);
}

getVehicles(filter: any): Observable<any> {
  return this.http.get<any>(this.getVehiclesEndpoint + '?' + this.toQueryString(filter))
    .pipe(
      catchError(error => {
        console.error('Error fetching vehicles', error);
        return throwError(error);
      })
    );
}

toQueryString(obj: any) {
  var parts = [];
  for (var property in obj) {
    var value = obj[property];
    if (value != null && value != undefined)
      parts.push(encodeURIComponent(property) + '=' + encodeURIComponent(value));
  }
  return parts.join('&');
}




}
