import { Component } from '@angular/core';
import { VehicleService } from 'src/app/services/vehicle.service';
import { MakeViewModel } from 'src/app/models/MakeViewModel';

@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css']
})
export class VehicleFormComponent {
makes!: any[];
vehicle: any = {};
models!: any[];
features!: any[];

constructor(
  private vehicleService: VehicleService) {}

ngOnInit() {
  this.vehicleService.getMakes().subscribe(result => {
    this.makes = result;
  })

  this.vehicleService.getFeatures().subscribe(result => {
    this.features = result;
  })
}

onMakeChange() {
var selectedMake = this.makes.find(m => m.id == this.vehicle.makeId);
this.models = selectedMake ? selectedMake.models : [];
delete this.vehicle.modelId;
}


}

