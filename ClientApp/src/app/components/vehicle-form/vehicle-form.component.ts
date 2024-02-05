import { Component } from '@angular/core';
import { MakeService } from 'src/app/services/make.service';
import { MakeViewModel } from 'src/app/models/MakeViewModel';

@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css']
})
export class VehicleFormComponent {
makes: MakeViewModel[] = [];

constructor(private makeService: MakeService) {}

ngOnInit() {
  this.makeService.getMakes().subscribe(makes => {
    this.makes = makes;
  })
  console.log("makes: ", this.makes)
}


}

