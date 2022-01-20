import { Component, OnInit, Input, OnChanges, SimpleChanges } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GlobalService } from "../services/global.service";
import { CarService } from "../services/car.service";
import { CarItem } from "../models/car-item";

import * as $ from "jquery";

@Component({
  selector: 'app-cars',
  templateUrl: './cars.component.html',
  styleUrls: ['./cars.component.css']
})
export class CarsComponent implements OnChanges, OnInit {
  //todo: bind these properties to child component
  carId: number = 0;
  carList: CarItem[] = [];

  constructor(private gs: GlobalService, private route: ActivatedRoute, private carService: CarService) { }

  ngOnInit(): void {
    this.refreshData(true);
  }

  ngOnChanges(changes: SimpleChanges): void {

  }

  /**
   * this will trigger onchanges event on car component
   * 
   * @param value
   */
  setActiveCar(newCarId: number) {
    this.carId = newCarId;
  }

  /**
   * todo: invoke API, then print results in html.
   * Then refresh input controls.
   *
   * 
   * */
  async loadData() {
    const s = await this.carService.
      getCarList().toPromise();
    const stringify = await JSON.stringify(s);
    this.carList = await JSON.parse(stringify);
  }

  /**
   * refresh data,
   * then trigger change on car id.
   * then display details on car component.
   *
   * */
  public async refreshData(isDisplayingFirstRow: boolean) {
    await this.loadData();

    if (!isDisplayingFirstRow) {
      return;
    }

    if (this.carList.length <= 0) {
      this.carId = 0;
      return;
    }

    this.carId = this.carList[0].Id;
  }

  /**
   * find car by id,
   * 
   * then confirm deletion.
   *
   * if user click yes, then delete car by id.
   *
   * then alert success message.
   * 
   * @param id
   */
  async deleteCar(id: number) {
    var message: string;
    const found = await this.carService.getCarById(id);
    if (found == null) {
      alert(this.gs.carNotFoundErrorMessage);
      return;
    }

    const deletedName = found.Name;
    
    message = this.gs.formatString(this.gs.carDeleteConfirmMessage, deletedName);
    const confirmed = confirm(message);
    if (!confirmed) {
      return;
    }

    //delete data
    const ids = [id];
    const isSuccessful = await this.carService.deleteCars(ids);
    if (isSuccessful) {
      return;
    }

    //refresh data
    this.refreshData(true);

    message = this.gs.formatString(this.gs.carDeletedSuccessfullyMessage, deletedName);
    alert(message);
  }
}
