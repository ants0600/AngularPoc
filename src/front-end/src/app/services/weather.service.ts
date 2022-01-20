import { Injectable } from '@angular/core';
import { GlobalService } from "./global.service";
import { HttpClient } from "@angular/common/http";
import { from, Observable } from 'rxjs';
import * as $ from "jquery";
import { AppSettings } from "../models/app-settings";

@Injectable({
  providedIn: 'root'
})
export class WeatherService {
  private appSettings: AppSettings;

  constructor(private http: HttpClient, private gs: GlobalService) {
    this.appSettings = this.gs.getAppSettings();
  }

  /**
   * ex: invoke API:
   * http://localhost:5000/api/Weather/GetWeatherByCity?city=jakarta
   * 
   * @param city
   */
  getWeatherByCity(city: string) {
    const serviceUrl = this.gs.getServiceUrl();
    var url = serviceUrl + this.appSettings.GetWeatherByCity;
    url = this.gs.formatString(url, city);
    return this.http.get(url);
  }
}
