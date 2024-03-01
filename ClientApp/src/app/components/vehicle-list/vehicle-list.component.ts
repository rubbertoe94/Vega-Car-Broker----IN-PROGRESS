import { Component } from '@angular/core';
import { VehicleService } from 'src/app/services/vehicle.service';
import { VehicleViewModel } from 'src/app/models/VehicleViewModels'; 
import { KeyValuePair } from 'src/app/models/VehicleViewModels';

@Component({
  selector: 'app-vehicle-list',
  templateUrl: './vehicle-list.component.html',
  styleUrl: './vehicle-list.component.css'
})
export class VehicleListComponent {
vehicles!: VehicleViewModel[]; 
makes!: KeyValuePair[];
filter: any = {};

constructor(private vehicleService: VehicleService) { }

ngOnInit() {
this.vehicleService.getMakes().subscribe(data => {
  this.makes = data;
});

  this.vehicleService.getVehicles().subscribe(data => {
    this.vehicles = data;
  });
} 

onFilterChange() {
  //prepare to implement the filter
}



}
