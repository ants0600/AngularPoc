import { Injectable } from '@angular/core';
import { GlobalService } from "./global.service";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { ContactUsItem } from "../models/contact-us-item";

@Injectable({
  providedIn: 'root'
})
export class ContactService {

  constructor(private gs: GlobalService, private http: HttpClient) { }

  async add(inserted: ContactUsItem): Promise<boolean> {
    const data = JSON.stringify(inserted);
    const appSettings = this.gs.getAppSettings();
    const serviceUrl = this.gs.getServiceUrl();
    const url = serviceUrl + appSettings.AddContactUs;

    const httpOptions = this.gs.postHttpOptions;

    const s = await this.http.post(url, data, httpOptions).toPromise();
    const result = JSON.stringify(s);
    const isSuccessful = JSON.parse(result);
    return isSuccessful;
  }
}
