import { Component, OnInit, Renderer2 } from '@angular/core';
import { UrlRecord } from '../shared/models/urlRecord';
import { UrlRecordService } from '../shared/services/url-record.service';
import { FormControl } from '@angular/forms';
import { TokenProviderService } from '../shared/services/token-provider.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})

export class HomeComponent implements OnInit {

  constructor(private urs: UrlRecordService, private tps: TokenProviderService) { }

  ngOnInit(): void {
    this.urs.getUrlRecords().subscribe(res => {
      this.urlRecords = res;
    }, error => {
      console.log('Something went wrong');
    });
  }

  onFilterMyRecords() {
    this.isMyRecordsFilterActive = !this.isMyRecordsFilterActive ;
    console.log("Test");
  }

  searchForm = new FormControl("");
  originalUrlForm = new FormControl("");

  isMyRecordsFilterActive = false;
  isAddNewActive = false;
  isViewActive = false;

  public urlRecords: UrlRecord[] = [];

  public get getFilteredList(): UrlRecord[] {
    let filteredList = this.urlRecords;
    let search = this.searchForm.value;
    let username = this.tps.getUsername();

    if(this.isMyRecordsFilterActive)
    {
      filteredList = filteredList.filter(item => item.userName !== username);
    }

    filteredList = filteredList.filter(item => item.originalUrl.includes(search) || item.shortUrl.includes(search));

    return filteredList;
  }

  public get isUnauthorised(): boolean {
    return this.tps.getRole() === "Unauthorized";
  }

  public get isAdmin(): boolean {
    return this.tps.getRole() === "Admin";
  }

  public get getUsername(): string {
    return this.tps.getUsername();
  }

  public canDelete(element: UrlRecord): boolean {
    return this.tps.getRole() === "Admin" || element.userName == this.tps.getUsername();
  }

  deleteRecord(id: string)
  {
    this.urs.deleteUrlRecord(id);

    this.urlRecords = this.urlRecords.filter(item => item.id !== id);
  }

  hideAddRecord()
  {
    this.isAddNewActive = false;
  }

  showAddRecord()
  {
    this.isAddNewActive = true;
  }

  addRecord()
  {

    let originalUrl = this.originalUrlForm.value;
    
    this.urs.addUrlRecord(originalUrl).subscribe(res => {
      let record = res;

      this.urlRecords = [...this.urlRecords, record];

      this.isAddNewActive = false;
    }, error => {
      console.log('Something went wrong');

      this.isAddNewActive = false;
    });
  }
}
