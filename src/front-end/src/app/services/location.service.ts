import { Injectable } from '@angular/core';
import { GlobalService } from "./global.service";
import { HttpClient } from "@angular/common/http";
import { from, Observable } from 'rxjs';
import * as $ from "jquery";
import { AppSettings } from "../models/app-settings";

@Injectable({
  providedIn: 'root'
})
export class LocationService {
  private appSettings: AppSettings;

  constructor(private http: HttpClient, private gs: GlobalService) {
    this.appSettings = this.gs.getAppSettings();
  }

  /**
   * ex: download string from:
   * http://localhost:5000/api/Location/GetCountries
   * 
   * */
  getCountries() {
    const serviceUrl = this.gs.getServiceUrl();
    var url = serviceUrl + this.appSettings.GetCountries;
    return this.http.get(url);
  }

  /**
   * ex: download string from:
   * http://localhost:5000/api/Location/GetCitiesByCountryName?country=indonesia
   * 
   * @param country
   */
  getCitiesByCountryName(country: string) {
    const serviceUrl = this.gs.getServiceUrl();
    var url = serviceUrl + this.appSettings.GetCitiesByCountryName;
    url = this.gs.formatString(url, country);
    return this.http.get(url);
  }
}
