import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-masthead',
  templateUrl: './masthead.component.html',
  styleUrls: ['./masthead.component.css']
})
export class MastheadComponent implements OnInit {
  title = "My masthead component";
  images = [
    {
      "url": "/assets/image/book.png"
    },
    {
      "url": "/assets/image/clock.png"
    }];

  constructor() { }

  ngOnInit(): void {
  }

}
