import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';


@Injectable({
    providedIn: 'root'
})
export class PhotoService {
    private readonly baseUrl: string = "https://localhost:7220/api";
    private readonly vehicleEndpoint: string = this.baseUrl + "/vehicles";

    constructor(private http: HttpClient) {}


    getPhotos(vehicleId: number) {
        return this.http.get(this.vehicleEndpoint + `/${vehicleId}/photos`);
    }

    upload(vehicleId: number, photo: any) {
        var formData = new FormData();
        formData.append('file', photo); //paramater name in the controller API is 'file', so it must be the same in the first argument of the append method
        return this.http.post(this.vehicleEndpoint + `/${vehicleId}/photos`, formData); 
    }
}