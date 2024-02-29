import { Component } from '@angular/core';
import { VehicleService } from 'src/app/services/vehicle.service';
import { MakeViewModel } from 'src/app/models/MakeViewModel';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { forkJoin } from 'rxjs';
import { SaveVehicleViewModel, VehicleViewModel } from 'src/app/models/VehicleViewModels';


@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css']
})
export class VehicleFormComponent {
makes!: any[];
models!: any[];
features!: any[];
vehicle: SaveVehicleViewModel = {
  id: 0,
  makeId: 0,
  modelId: 0,
  isRegistered: false,
  features: [],
  contact: {
    name: '',
    email: '',
    phone: ''
  }
};

constructor(
  private route: ActivatedRoute,
  private router: Router,
  private vehicleService: VehicleService,
  private toastr: ToastrService) {
    route.params.subscribe(p => {
      this.vehicle.id = +p['id'];
    });
  }

ngOnInit() {
var sources = [
  this.vehicleService.getMakes(),
  this.vehicleService.getFeatures(),];

  if (this.vehicle.id)
    sources.push(this.vehicleService.getVehicle(this.vehicle.id));
  
const combined = forkJoin(sources).subscribe(data => {
    this.makes = data[0];
    this.features = data[1];
    if (this.vehicle.id) {
      this.setVehicle(data[2]);
      this.populateModels();
    };
  }, err => {
    if (err.status == 404) {
      this.router.navigate(['/home']);
    }
  });
}

private setVehicle(v: VehicleViewModel) {
  this.vehicle.id = v.id;
  this.vehicle.makeId = v.make.id;
  this.vehicle.modelId = v.model.id;
  this.vehicle.isRegistered = v.isRegistered;
  this.vehicle.contact = v.contact;
  this.vehicle.features = v.features.map(f => f.id);
}


onMakeChange() {
  this.populateModels();
  delete (this.vehicle as any).modelId;
}

private populateModels() {
  var selectedMake = this.makes.find(m => m.id == this.vehicle.makeId);
  this.models = selectedMake ? selectedMake.models : [];
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
    .subscribe(x => console.log(x));
}



}

