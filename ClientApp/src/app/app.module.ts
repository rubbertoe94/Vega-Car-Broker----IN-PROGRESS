
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule, Component, ErrorHandler } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';

import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { VehicleListComponent } from './components/vehicle-list/vehicle-list.component';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { VehicleFormComponent } from './components/vehicle-form/vehicle-form.component';
import { VehicleService } from './services/vehicle.service';
import { AppErrorHandler } from './app.error-handler';

import { NgxPaginationModule } from 'ngx-pagination';
import { PaginationComponent } from './components/pagination/pagination.component';
import { ViewVehicleComponent } from './components/vehicle-view/vehicle-view.component';
import { NgbModule, NgbNavModule } from '@ng-bootstrap/ng-bootstrap';





@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    VehicleFormComponent,
    VehicleListComponent,
    PaginationComponent,
    ViewVehicleComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    NgxPaginationModule,
    HttpClientModule,
    FormsModule,
    NgbModule,
    NgbNavModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot({
      timeOut: 5000,
      positionClass: 'toast-bottom-right',
    }),
    RouterModule.forRoot([
      { path: '', redirectTo: '/home', pathMatch: 'full' },
      { path: 'home', component: HomeComponent },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'vehicles/all', component: VehicleListComponent },
      { path: 'vehicles/new', component: VehicleFormComponent },
      { path: 'vehicles/edit/:id', component: VehicleFormComponent },
      { path: 'vehicles/:id', component: ViewVehicleComponent },
      { path: '**', redirectTo: 'vehicles/all' }
    ])
  ],
  providers: [
    VehicleService, 
  { provide: ErrorHandler, useClass: AppErrorHandler }
],
  
  bootstrap: [AppComponent]
})
export class AppModule { }
