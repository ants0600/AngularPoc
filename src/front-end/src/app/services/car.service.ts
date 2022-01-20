"use strict";
import { Injectable } from '@angular/core';
import { GlobalService } from "./global.service";
import { HttpClient } from "@angular/common/http";
import { CarItem } from "../models/car-item";
import { from, Observable } from 'rxjs';
import * as $ from "jquery";
import { AppSettings } from "../models/app-settings";
@Injectable({
  providedIn: 'root'
})
export class CarService {
  private appSettings: AppSettings;

  /**
   *todo: read settings from appsettings.json
   * 
   * @param http
   * @param gs
   */
  constructor(private http: HttpClient, private gs: GlobalService) {
    this.appSettings = this.gs.getAppSettings();
  }

  /**
   *
   * get all car list from api
   * 
   */
  getCarList() {
    const serviceUrl = this.gs.getServiceUrl();
    var url = serviceUrl + this.appSettings.GetAllCars;
    return this.http.get(url);
  }

  getCarListByPageIndex(pageIndex: number, pageSize: number, orderBy: number, isAscending: boolean) {
    const serviceUrl = this.gs.getServiceUrl();
    var url = serviceUrl + this.appSettings.GetCarsByPageIndex;
    const pageIndexValue = `${pageIndex}`;
    const pageSizeValue = `${pageSize}`;
    const orderByValue = `${orderBy}`;
    const isAscendingValue = `${isAscending}`;
    url = this.gs.formatString(url, pageIndexValue, pageSizeValue, orderByValue, isAscendingValue);
    return this.http.get(url);
  }

  getCarPageCount(pageSize: number) {

    const serviceUrl = this.gs.getServiceUrl();
    var url = serviceUrl + this.appSettings.GetCarsPageCount;
    const pageSizeValue = `${pageSize}`;
    url = this.gs.formatString(url, pageSizeValue);
    return this.http.get(url, { responseType: 'text' });
  }

  private getCarByNameFromApi(name: string) {
    const serviceUrl = this.gs.getServiceUrl();
    var url = serviceUrl + this.appSettings.GetCarByName;
    url = this.gs.formatString(url, name);
    return this.http.get(url);

  }

  private getCarByIdFromApi(id: number) {
    const serviceUrl = this.gs.getServiceUrl();
    var url = serviceUrl + this.appSettings.GetCarById;
    var idValue = `${id}`;
    url = this.gs.formatString(url, idValue);
    return this.http.get(url);

  }

  async getCarById(id: number): Promise<CarItem> {
    const s = await this.getCarByIdFromApi(id).toPromise();
    const stringify = JSON.stringify(s);
    if (stringify === this.gs.nullValue) {
      return new CarItem();
    }

    const value = JSON.parse(stringify);
    return value;
  }

  async addCar(inserted: CarItem): Promise<boolean> {
    const data = JSON.stringify(inserted);
    const appSettings = this.gs.getAppSettings();
    const serviceUrl = this.gs.getServiceUrl();
    const url = serviceUrl + appSettings.AddCar;

    const httpOptions = this.gs.postHttpOptions;

    const s = await this.http.post(url, data, httpOptions).toPromise();
    const result = JSON.stringify(s);
    const value = JSON.parse(result);
    const status = value.Status;
    return !!status;
  }

  async updateCar(updated: CarItem): Promise<boolean> {
    const data = JSON.stringify(updated);
    const appSettings = this.gs.getAppSettings();
    const serviceUrl = this.gs.getServiceUrl();
    const url = serviceUrl + appSettings.UpdateCar;

    const httpOptions = this.gs.postHttpOptions;

    const s = await this.http.post(url, data, httpOptions).toPromise();
    const result = JSON.stringify(s);
    const value = JSON.parse(result);
    const status = value.Status;
    return !!status;
  }

  async deleteCars(ids: number[]): Promise<boolean> {
    const data = JSON.stringify(ids);
    const appSettings = this.gs.getAppSettings();
    const serviceUrl = this.gs.getServiceUrl();
    const url = serviceUrl + appSettings.DeleteCarByIds;

    const httpOptions = this.gs.postHttpOptions;

    const s = await this.http.post(url, data, httpOptions).toPromise();
    const result = JSON.stringify(s);
    const value = JSON.parse(result);
    const status = value.Status;
    return !!status;
  }

  validateCar(updated: CarItem): string {
    updated.Name = (`${updated.Name}`).trimStart().trimEnd();

    var value = "";
    if (updated.Name === "") {
      value += "Please fill Name.";
      value += "\n";
    }

    const updatedYear = updated.Year;
    const updatedPrice = updated.Price;
    const updatedYearValue = `${updatedYear}`;
    const updatedPriceValue = `${updatedPrice}`;

    if (updatedYear <= 0 || updatedYearValue === this.gs.notNumber) {
      value += "Please fill Year.";
      value += "\n";
    }

    if (updatedPrice <= 0 || updatedPriceValue === this.gs.notNumber) {
      value += "Please fill Price.";
      value += "\n";
    }

    return value;
  }

  getSortByList(): string[] {
    var values: string[] = [];
    values.push(this.gs.textCreatedDateNewest);
    values.push(this.gs.textCreatedDateOldest);
    values.push(this.gs.textNameAscending);
    values.push(this.gs.textNameDescending);
    values.push(this.gs.textPriceHighest);
    values.push(this.gs.textPriceLowest);
    values.push(this.gs.textYearNewest);
    values.push(this.gs.textYearOldest);
    return values;
  }

  getOrderBy(sortBy: string): number {
    switch (sortBy) {
      //id
      case this.gs.textCreatedDateOldest:
      case this.gs.textCreatedDateNewest:
        {
          return this.gs.orderById;
        }
      //year
      case this.gs.textYearNewest:
      case this.gs.textYearOldest:
        {
          return this.gs.orderByYear;
        }
      //name
      case this.gs.textNameAscending:
      case this.gs.textNameDescending:
        {
          return this.gs.orderByName;
        }
      case this.gs.textPriceHighest:
      case this.gs.textPriceLowest:
        {
          return this.gs.orderByPrice;
        }
    }

    return this.gs.orderById;
  }

  getIsAscending(newSortBy: string): boolean {
    switch (newSortBy) {
      case this.gs.textCreatedDateOldest:
      case this.gs.textYearOldest:
      case this.gs.textNameAscending:
      case this.gs.textPriceLowest:
        {
          return true;
        }
    }

    return false;
  }
}
