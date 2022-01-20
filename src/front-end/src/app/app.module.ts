import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

//todo: import all components
import { AppComponent } from './app.component';
import { MastheadComponent } from './masthead/masthead.component';
import { CarouselComponent } from './carousel/carousel.component';
import { NavbarComponent } from './navbar/navbar.component';
import { SampleformComponent } from './sampleform/sampleform.component';
import { AboutusComponent } from './aboutus/aboutus.component';
import { ContactusComponent } from './contactus/contactus.component';
import { AppRoutingModule } from './app-routing.module';
import { HomeComponent } from './home/home.component';
import { CarsComponent } from './cars/cars.component';
import { CarComponent } from './car/car.component';
import { PagingcarsComponent } from './pagingcars/pagingcars.component';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { WeatherComponent } from './weather/weather.component';

//todo: must include all components
@NgModule({
  declarations: [
    AppComponent,
    MastheadComponent,
    CarouselComponent,
    NavbarComponent,
    SampleformComponent,
    AboutusComponent,
    ContactusComponent,
    HomeComponent,
    CarsComponent,
    CarComponent,
    PagingcarsComponent,
    WeatherComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
