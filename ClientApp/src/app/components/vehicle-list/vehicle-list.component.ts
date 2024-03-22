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
query: any = {
  page: 1,
  pageSize: 3
};
totalVehicles!: number;
totalPages!: number;
pageSizes: number[] = [3, 6, 9];




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
  });
  
}

onFilterChange() {
  this.query.page = 1;
 this.populateVehicles();
}

resetFilter() {
  this.query = {
    page: this.query.page,
    pageSize: this.query.pageSize
  };
  this.populateVehicles();
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
  this.query.page = newPage;
  this.populateVehicles();
}

onPageSizeChange(newSize: any) {
  this.query.pageSize = newSize.target.value;
  this.query.page = 1;
  this.populateVehicles();
}



}


