import { ToastrService } from 'ngx-toastr';
import { VehicleService } from './../../services/vehicle.service';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { VehicleViewModel } from 'src/app/models/VehicleViewModels';
import { PhotoService } from 'src/app/services/photo.service';


@Component({
  selector: 'app-vehicle-view',
  templateUrl: './vehicle-view.component.html',
  styleUrls: ['./vehicle-view.component.css']
})
export class ViewVehicleComponent implements OnInit {
  @ViewChild('fileInput') fileInput!: ElementRef;
  vehicle: any;
  vehicleId!: number; 

  constructor(
    private route: ActivatedRoute, 
    private router: Router,
    private toastr: ToastrService,
    private vehicleService: VehicleService,
    private photoService: PhotoService) { 

    route.params.subscribe(p => {
      this.vehicleId = +p['id'];
      if (isNaN(this.vehicleId) || this.vehicleId <= 0) {
        router.navigate(['/vehicles']);
        return; 
      }
    });
  }

  ngOnInit() { 
    this.vehicleService.getVehicle(this.vehicleId)
      .subscribe(
        v => this.vehicle = v,
        err => {
          if (err.status == 404) {
            this.router.navigate(['/vehicles']);
            return; 
          }
        });
  }

  delete() {
    if (confirm("Are you sure?")) {
      this.vehicleService.delete(this.vehicle.id)
        .subscribe(x => {
          this.router.navigate(['/vehicles']);
        });
    }
  }


uploadPhoto() {
  var nativeElement: HTMLInputElement = this.fileInput.nativeElement;
  this.photoService.upload(this.vehicleId, nativeElement.files![0])
    .subscribe(x => {console.log(x)});
}




}