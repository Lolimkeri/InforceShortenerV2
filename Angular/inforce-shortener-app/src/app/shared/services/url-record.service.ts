import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs/internal/Observable';
import { UrlRecord } from '../models/urlRecord';
import { tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UrlRecordService {

  private baseUrl: string = environment.baseUrl;
  
  private urlRecordsUrl: string = this.baseUrl + '/url';

  constructor(private http: HttpClient) { }

  getUrlRecords(): Observable<UrlRecord[]> {
    return this.http.get<UrlRecord[]>(this.urlRecordsUrl).pipe();
  }

  deleteUrlRecord(id: string) {
    this.http.delete(this.urlRecordsUrl + "/" + id).pipe().subscribe();
  }

  addUrlRecord(originalUrl: string): Observable<UrlRecord> {
    return this.http.post<UrlRecord>(this.urlRecordsUrl, {originalUrl}).pipe(tap())
  }
}
