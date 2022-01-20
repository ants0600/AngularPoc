import { isDevMode, Injectable } from '@angular/core';
import { AppSettings } from "../models/app-settings";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import * as $ from "jquery";
@Injectable({
  providedIn: 'root'
})

export class GlobalService {

  //all constant values
  //0: id, 1: Name, 2: Price, 3: Year
  orderById = 0;
  orderByName = 1;
  orderByPrice = 2;
  orderByYear = 3;
  city = "#city";
  cityName = "#cityName";
  cbSortBy = "#cbSortBy";
  textCreatedDateNewest = "Created Date (newest)";
  textCreatedDateOldest = "Created Date (oldest)";
  textNameAscending = "Name (A-Z)";
  textNameDescending = "Name (Z-A)";
  textPriceHighest = "Price (highest)";
  textPriceLowest = "Price (lowest)";
  textYearNewest = "Year (newest)";
  textYearOldest = "Year (oldest)";

  carOrderBy = 0;
  carNotFoundErrorMessage = "The selected car is not found, please refresh the list";
  cbPageIndexQuery = "#cbPageIndex";
  carPageSize: number = 10;
  notNumber = "NaN";
  carDeletedSuccessfullyMessage = "The car of [{0}] is deleted successfully";
  carDeleteConfirmMessage = "Are you sure want to delete a car of [{0}]?";
  carUpdateSuccessMessage = "The car of [{0}] is successfully updated";
  carInsertSuccessMessage = "A new car of [{0}] is successfully inserted";
  hideCss = "hide";
  country = "#country";
  btResetCar = " .btResetCar";
  btRefreshCarQuery = " .btRefreshCar";
  btNewCarQuery = " .btNewCar";
  btEditCarQuery = " .btEditCar";
  btSaveCarQuery = " .btSaveCar";
  linkQuery: string = "#link";
  readOnlyAttribute = "readonly";
  formQuery = "form";
  cssWasValidated = "was-validated";
  invalidQuery = "form :invalid";
  private appSettings: string = "/assets/configurations/appsettings.json";
  carNameSelector: string = "#carId";
  carYearSelector: string = "#carYear";
  carPriceSelector: string = "#carPrice";
  fullNameQuery = "#fullName";
  emailQuery = "#email";
  messageQuery = "#message";
  successPanelQuery = "#success";
  failurePanelQuery = "fail";
  private applicationJson = "application/json";
  postHttpOptions: any = {
    headers: new HttpHeaders({
      'Content-Type': this.applicationJson,
      'Accept': this.applicationJson
    })
  }

  currentCarId: string | null | number | string[] | undefined = "";
  nullValue = `${null}`;
  private jsonElementFormat = " '{0}':'{1}' ";

  constructor() { }

  getAppSettings(): AppSettings {
    var value = new AppSettings();
    var url = this.appSettings;
    $.ajax({
      url: url,
      async: false,
      success: function (result) {
        const stringify = JSON.stringify(result);
        value = JSON.parse(stringify);
      }
    });

    return value;
  }

  getServiceUrl(): string {
    const appSettings = this.getAppSettings();
    if (isDevMode()) {
      return appSettings.ServiceUrlDev;
    }

    return appSettings.ServiceUrlProd;
  }

  getString(value: string | null | number | string[] | undefined) {
    return value === null || !value ? "" : value;
  }

  formatString(str: string, ...val: string[]): string {
    for (let index = 0; index < val.length; index++) {
      str = str.replace(`{${index}}`, val[index]);
    }
    return str;
  }

  getFormData($form: any): string {
    const unindexedArray = $form.serializeArray();

    let value = "{";
    const count = unindexedArray.length;
    for (let i = 0; i < count; ++i) {
      const item = unindexedArray[i];
      const itemName = item['name'];
      const itemValue = item['value'];
      const s = this.formatString(this.jsonElementFormat, itemName, itemValue);
      value += s;
      if (i < count - 1) {
        value += ",";
      }
    }
    value += "}";
    return value;
  }

  celcius = "#celcius";
  farenheit = "#farenheit";
  latitude = "#latitude";
  longitude = "#longitude";
  skyConditions = "#skyConditions";
  dateTime = "#dateTime";
  visibility = "#visibility";
  windDegree = "#windDegree";
  windSpeed = "#windSpeed";
}
