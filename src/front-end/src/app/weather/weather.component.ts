import { Component, OnInit } from '@angular/core';
import { GlobalService } from "../services/global.service";
import { LocationService } from "../services/location.service";
import { WeatherService } from "../services/weather.service";
import { Country } from "../models/country";
import { CityItem } from "../models/city-item";
import { WeatherItem } from "../models/weather-item";

import * as $ from "jquery";

@Component({
  selector: 'app-weather',
  templateUrl: './weather.component.html',
  styleUrls: ['./weather.component.css']
})
export class WeatherComponent implements OnInit {
  weather: WeatherItem | any = null;
  country: string = "";
  city: string = "";
  countries: Country[] = [];
  cities: CityItem[] = [];

  constructor(private gs: GlobalService, private locationService: LocationService, private weatherService: WeatherService) {

  }

  ngOnInit(): void {
    this.loadData();
  }

  async loadData() {
    await this.refreshCountries();
    if (this.countries !== null) {
      if (this.countries.length > 0) {
        await this.getCitiesByCountryName(this.countries[0].Name);
      }
    }
  }

  /**
   * refresh country list, for drop down
   * */
  async refreshCountries() {
    const s = await this.locationService.getCountries().toPromise();
    const stringify = await JSON.stringify(s);
    this.countries = await JSON.parse(stringify);
  }

  refreshAll() {
    this.loadData();
  }

  changeCountry() {
    this.getCities();
  }

  async getCities() {
    this.country = `${$(this.gs.country).val()}`;
    await this.getCitiesByCountryName(this.country);
  }

  async getCitiesByCountryName(countryName: string) {
    const s = await this.locationService.getCitiesByCountryName(countryName).toPromise();
    const stringify = await JSON.stringify(s);
    this.cities = await JSON.parse(stringify);

  }

  changeCity() { }

  submit() {
    this.displayWeather();
  }

  /**
   * invoke API, then display in page
   * */
  async displayWeather() {
    this.city = `${$(this.gs.city).val()}`;
    const s = await this.weatherService.getWeatherByCity(this.city).toPromise();
    const stringify = await JSON.stringify(s);
    this.weather = await JSON.parse(stringify);
    if (this.weather != null) {
      const weatherData = this.weather.WeatherData;
      if (weatherData != null) {
        $(this.gs.celcius).val(weatherData.Celcius);
        $(this.gs.cityName).val(weatherData.CityName);
        $(this.gs.farenheit).val(weatherData.Farenheit);
        $(this.gs.latitude).val(weatherData.Latitude);
        $(this.gs.longitude).val(weatherData.Longitude);
        $(this.gs.skyConditions).val(weatherData.SkyConditions);
        $(this.gs.dateTime).val(weatherData.UtcDateTimeLongFormat);
        $(this.gs.visibility).val(weatherData.Visibility);
        $(this.gs.windDegree).val(weatherData.WindDegree);
        $(this.gs.windSpeed).val(weatherData.WindSpeed);
      }
    }


  }
}
