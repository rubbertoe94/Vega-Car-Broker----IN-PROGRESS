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
allVehicles!: VehicleViewModel[];

constructor(private vehicleService: VehicleService) { }

ngOnInit() {
this.vehicleService.getMakes().subscribe(data => {
  this.makes = data;
});

  this.vehicleService.getVehicles().subscribe(data => {
    this.vehicles = this.allVehicles = data;
  });
} 

onFilterChange() {
  //prepare to implement the filter
  var vehicles = this.allVehicles;
  if (this.filter.makeId) {
  vehicles = vehicles.filter(v => v.make.id == this.filter.makeId);
  }
  this.vehicles = vehicles;
}

resetFilter() {
  this.filter = {};
  this.onFilterChange();
}

}
