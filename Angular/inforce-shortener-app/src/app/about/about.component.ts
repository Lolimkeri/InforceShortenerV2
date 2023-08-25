import { Component, OnInit } from '@angular/core';
import { TextProviderService } from '../shared/services/text-provider.service';
import { TokenProviderService } from '../shared/services/token-provider.service';
import { FormControl } from '@angular/forms';

export const ABOUT_NAME = 'About'

@Component({
  selector: 'app-about',
  templateUrl: './about.component.html',
  styleUrls: ['./about.component.scss']
})
export class AboutComponent implements OnInit {

  constructor(private tps: TextProviderService, private tokenps: TokenProviderService) { 
    this.tps.getText(ABOUT_NAME).subscribe(res => {
      this.mainMessage = res.textValue;
    }, error => {
      this.mainMessage = 'Unexpected error occured. Please try again later.';
    })
  }

  ngOnInit(): void {
  }
 
  public mainMessage: string = 'Loading...';
  public textAreaForm = new FormControl(this.mainMessage);

  edit()
  {
    this.isEdit = !this.isEdit;

    if(!this.isEdit)
    {
      this.mainMessage = this.textAreaForm.value;
      this.tps.editText(ABOUT_NAME, this.textAreaForm.value);
    }
    else
    {
      this.textAreaForm.setValue(this.mainMessage);
    }
  }

  isEdit: boolean = false;

  public get isAdmin(): boolean {
    return this.tokenps.getRole() === "Admin";
  }
}
