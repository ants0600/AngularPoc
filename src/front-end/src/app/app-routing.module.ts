import { NgModule, Component, OnInit } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AboutusComponent } from "./aboutus/aboutus.component";
import { HomeComponent } from "./home/home.component";
import { ContactusComponent } from "./contactus/contactus.component";
import { CarsComponent } from "./cars/cars.component";
import { CarComponent } from "./car/car.component";
import { PagingcarsComponent } from "./pagingcars/pagingcars.component";
import { WeatherComponent } from "./weather/weather.component";

declare const loadPage: any;

//todo: define routing here for all components / pages
const routes: Routes = [
  {
    path: "aboutus",
    component: AboutusComponent
  },
  {
    path: "home",
    component: HomeComponent
  },
  {
    path: "contactus",
    component: ContactusComponent
  },
  {
    path: "pagingcars",
    component: PagingcarsComponent
  },
  {
    path: "cityweather",
    component: WeatherComponent
  },
  {
    path: "cars",
    component: CarsComponent,
    children: [
      {
        //todo: dynamic routing, with parameter id
        path: "car/:id",
        component: CarComponent
      }
    ]
  },
  {
    path: '',
    redirectTo: '/home',
    pathMatch: 'full'
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {

}
