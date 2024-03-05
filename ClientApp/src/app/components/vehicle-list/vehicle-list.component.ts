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
query: any = {};

constructor(private vehicleService: VehicleService) { }

ngOnInit() {
this.vehicleService.getMakes().subscribe(data => {
  this.makes = data;
});
this.populateVehicles();
} 

private populateVehicles() {
  this.vehicleService.getVehicles(this.query).subscribe(data => {
    this.vehicles = data;
  });

}

onFilterChange() {
 this.populateVehicles();
}

resetFilter() {
  this.query = {};
  this.onFilterChange();
}

sortBy(columnName: string) {
  if (this.query.sortBy === columnName) {
    this.query.isSortAscending = !this.query.isSortAscending;
  } else {
    this.query.sortBy = columnName;
    this.query.isSortAscending = true;
  }
  this.populateVehicles();
}



}
