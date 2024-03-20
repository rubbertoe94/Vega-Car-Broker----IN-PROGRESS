import { Component } from '@angular/core';
import { VehicleService } from 'src/app/services/vehicle.service';
import { VehicleViewModel } from 'src/app/models/VehicleViewModels'; 
import { KeyValuePair } from 'src/app/models/VehicleViewModels';
import { PaginationComponent } from '../pagination/pagination.component';

@Component({
  selector: 'app-vehicle-list',
  templateUrl: './vehicle-list.component.html',
  styleUrl: './vehicle-list.component.css'
})
export class VehicleListComponent {
vehicles!: VehicleViewModel[]; 
makes!: KeyValuePair[];
query: any = {
  page: 1,
  pageSize: 2
};
totalVehicles!: number;
totalPages!: number;



constructor(private vehicleService: VehicleService) { }

ngOnInit() {
this.vehicleService.getMakes().subscribe(data => {
  this.makes = data;
});
this.populateVehicles();
} 

private populateVehicles() {
  this.vehicleService.getVehicles(this.query).subscribe(data => {
    this.vehicles = data.vehicles;
    this.totalVehicles = data.totalItems;
    this.totalPages = data.totalPages;

  // console.log("vehicles: ", this.vehicles);
  // console.log("totalItems: ", this.totalVehicles);
  // console.log("totalPages: ", this.totalPages);
  // console.log("query: ", this.query);
  });
  
}

onFilterChange() {
 this.populateVehicles();
}

resetFilter() {
  this.query = {
    page: this.query.page,
    pageSize: this.query.pageSize
  };
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

onPageChange(newPage: number) {
  // console.log(`Changing to page ${newPage}`);
  this.query.page = newPage;
  // console.log(`Updated query: `, this.query);
  this.populateVehicles();
}



}


