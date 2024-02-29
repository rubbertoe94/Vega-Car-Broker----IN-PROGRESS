import { Component } from '@angular/core';
import { VehicleService } from 'src/app/services/vehicle.service';
import { MakeViewModel } from 'src/app/models/MakeViewModel';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css']
})
export class VehicleFormComponent {
makes!: any[];
models!: any[];
features!: any[];
vehicle: any = {
  features: [],
  contact: {}
};

constructor(
  private vehicleService: VehicleService,
  private toastr: ToastrService
  ) {}

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

onFeatureToggle(featureId: number, $event: Event) {
  //tells angular that 'checked' is a property of target
  const target = $event.target as HTMLInputElement | null;
  
  if (target && target.checked) {
    this.vehicle.features.push(featureId);
  } else {
    var index = this.vehicle.features.indexOf(featureId);
    this.vehicle.features.splice(index, 1);
  }
}

submit() {
  console.log(this.vehicle);
  this.vehicleService.create(this.vehicle)
    .subscribe(response => console.log(response));
}



}

