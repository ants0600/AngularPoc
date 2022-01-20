import { ViewContainerRef, Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GlobalService } from "../services/global.service";
import { CarService } from "../services/car.service";
import { CarItem } from "../models/car-item";
import { CarsComponent } from "../cars/cars.component";

import * as $ from "jquery";
@Component({
  selector: 'app-car',
  templateUrl: './car.component.html',
  styleUrls: ['./car.component.css']
})
export class CarComponent implements OnChanges {
  _parentCarsComponent: CarsComponent;
  updatedCarId: number = 0;
  carId: number = 0;
  private simpleChanges: any | null = null;

  private _car: CarItem = new CarItem();

  get Car(): CarItem { return this._car }

  @Input() activeCarId: number = 0;

  visible: boolean = false;

  constructor(private route: ActivatedRoute, private gs: GlobalService, private _service: CarService, private viewContainerRef: ViewContainerRef) {
    const _injector = this.viewContainerRef.parentInjector;
    this._parentCarsComponent = _injector.get<CarsComponent>(CarsComponent);


    this.carId = 0;

  }

  /**
   * get changes from parent component,
   * then invoke data displaying
   * 
   * @param changes
   */
  ngOnChanges(changes: SimpleChanges): void {
    this.enable(false);
    var stringify = JSON.stringify(changes);
    this.simpleChanges = JSON.parse(stringify);
    this.carId = parseInt(`${this.simpleChanges.activeCarId.currentValue}`);
    this.displayCarData();
  }

  async displayCarData() {
    this._car = await this._service.getCarById(this.activeCarId);
    if (this._car == null) {
      this.carId = 0;
      return true;
    }

    return true;
  }

  reset(isEnabled: boolean) {
    this.displayCarData();
    this.enable(isEnabled);
    $(this.gs.carNameSelector).val(this._car.Name);
    $(this.gs.carYearSelector).val(this._car.Year);
    $(this.gs.carPriceSelector).val(this._car.Price);
  }

  /**
   * prepare object,
   * validate form,
   * invoke service add
   *
   * */
  async save() {
    const isInserting = this.updatedCarId <= 0;

    //prepare object
    var updated = new CarItem();
    const updatedName = `${$(this.gs.carNameSelector).val()}`;
    const updatedPrice = `${$(this.gs.carPriceSelector).val()}`;
    const updatedYear = `${$(this.gs.carYearSelector).val()}`;

    updated.Id = this.updatedCarId;
    updated.Name = updatedName.trimStart().trimEnd();
    updated.Price = parseInt(updatedPrice);
    updated.Year = parseInt(updatedYear);

    //validate first
    const validateCar = this._service.validateCar(updated);
    if (validateCar !== "") {
      alert(validateCar);
      return;
    }

    //invoke service add / update
    const isSuccess = isInserting ? await this._service.addCar(updated) : await this._service.updateCar(updated);

    if (isSuccess) {
      const carInsertSuccessMessage = isInserting ? this.gs.carInsertSuccessMessage : this.gs.carUpdateSuccessMessage;
      const message = this.gs.formatString(carInsertSuccessMessage, updated.Name);
      alert(message);
      this._parentCarsComponent.refreshData(isInserting);
      this.reset(false);
    }
  }

  /**
   * will be invoked when we click edit.
   * Only enabling input controls.
   * And set flag = edit.
   * enable save button.
   *
   * */
  prepareEdit() {
    this.updatedCarId = this.Car.Id;
    this.reset(true);
    $(this.gs.carNameSelector).focus();
  }

  /**
   * will be invoked when we click new button.
   * set flag = new,
   * empty input controls,
   * then enable input controls
   * 
   * */
  prepareNew() {
    this.updatedCarId = 0;
    $(this.gs.carNameSelector).val("");
    $(this.gs.carYearSelector).val(0);
    $(this.gs.carPriceSelector).val(0);

    this.enable(true);

    $(this.gs.carNameSelector).focus();

  }

  enable(isEnabled: boolean) {
    let readOnlyAttribute = this.gs.readOnlyAttribute;
    if (isEnabled) {
      let value = "";
      $(this.gs.carNameSelector).prop(readOnlyAttribute, value);
      $(this.gs.carYearSelector).prop(readOnlyAttribute, value);
      $(this.gs.carPriceSelector).prop(readOnlyAttribute, value);
      $(this.gs.btSaveCarQuery).removeClass(this.gs.hideCss);

      return true;
    }

    $(this.gs.carNameSelector).prop(readOnlyAttribute, readOnlyAttribute);
    $(this.gs.carYearSelector).prop(readOnlyAttribute, readOnlyAttribute);
    $(this.gs.carPriceSelector).prop(readOnlyAttribute, readOnlyAttribute);
    $(this.gs.btSaveCarQuery).addClass(this.gs.hideCss);

    return true;
  }


}
