import { Component, OnInit } from '@angular/core';
import { GlobalService } from "../services/global.service";
import { CarService } from "../services/car.service";
import { CarItem } from "../models/car-item";

import * as $ from "jquery";


@Component({
  selector: 'app-pagingcars',
  templateUrl: './pagingcars.component.html',
  styleUrls: ['./pagingcars.component.css']
})
export class PagingcarsComponent implements OnInit {
  sortByList: string[] = [];
  carOrderBy: number;
  pageCount: number = 0;
  isAscending = false;
  pageSize: number;
  pageIndexList: number[] = [];
  pageIndex: number = 0;
  startRowNumber = 1;

  carList: CarItem[] = [];

  constructor(private gs: GlobalService, private carService: CarService) {
    this.carOrderBy = this.gs.orderById;
    this.pageSize = this.gs.carPageSize;
    this.sortByList = this.carService.getSortByList();
  }

  ngOnInit(): void {
    this.loadData();
  }

  async loadData() {
    await this.refreshPageCount();
    await this.refreshData();
  }

  changePageIndex() {
    const newPageIndex = `${$(this.gs.cbPageIndexQuery).val()}`;
    this.pageIndex = parseInt(newPageIndex);
    this.refreshData();

  }

  /**
   * get row.
   * get page count.
   * */
  async refreshData() {
    //get data source
    const s = await this.carService.getCarListByPageIndex(this.pageIndex,
      this.pageSize, this.carOrderBy, this.isAscending).
      toPromise();
    const stringify = await JSON.stringify(s);
    this.carList = await JSON.parse(stringify);
    this.startRowNumber = (this.pageIndex * this.pageSize) + 1;
  }

  /**
   * get page count, then prepare array, to be put in drop down list
   * */
  async refreshPageCount() {

    const s = await this.carService.getCarPageCount(this.pageSize).toPromise();

    const stringify = await JSON.stringify(s);
    const count = await JSON.parse(stringify);
    this.pageCount = parseInt(count);
    for (var i = 1; i <= this.pageCount; i++) {
      this.pageIndexList.push(i);
    }
  }

  changeSortBy() {
    const newSortBy = `${$(this.gs.cbSortBy).val()}`;
    this.carOrderBy = this.carService.getOrderBy(newSortBy);
    this.isAscending = this.carService.getIsAscending(newSortBy);
    this.refreshData();
  }

  refreshAll() {
    this.loadData();
  }
}
