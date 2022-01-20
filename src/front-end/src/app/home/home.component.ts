import { Component, OnInit } from '@angular/core';

//todo: import jquery if required
import * as $ from "jquery";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
    //todo: use JQUERY ONLY if really required
    $("#homePageHeader").html("Home Page");
  }

}
