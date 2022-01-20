import { Component, OnInit } from '@angular/core';
import { GlobalService } from "../services/global.service";
import { ContactUsItem } from "../models/contact-us-item";
import { ContactService } from "../services/contact.service";
import * as $ from "jquery";

@Component({
  selector: 'app-sampleform',
  templateUrl: './sampleform.component.html',
  styleUrls: ['./sampleform.component.css']
})

export class SampleformComponent implements OnInit {

  title = "sample form component";
  name = "sample form name";

  constructor(private gs: GlobalService, private service: ContactService) { }

  ngOnInit(): void {
  }

  //todo: create method to change property value
  setTitle(value: string) {
    this.title = value;
    $("#spanName").html(value);
  }

  /**
   * 
   * */
  async submit() {
    //validate
    const formElement = $(this.gs.formQuery);
    formElement.addClass(this.gs.cssWasValidated);
    const invalidElements = $(this.gs.invalidQuery);
    const invalidCount = invalidElements.length;
    if (invalidCount > 0) {
      return false;
    }

    //read form data
    const fullName = $(this.gs.fullNameQuery).val();
    const email = $(this.gs.emailQuery).val();
    const message = $(this.gs.messageQuery).val();

    //submit
    const inserted = new ContactUsItem();
    inserted.Email = `${email}`;
    inserted.FullName = `${fullName}`;
    inserted.Message = `${message}`;
    const isSuccess = await this.service.add(inserted);
    formElement.hide();
    if (isSuccess) {
      $(this.gs.successPanelQuery).show();
      return true;
    }

    $(this.gs.failurePanelQuery).show();
    return true;
  }
}
