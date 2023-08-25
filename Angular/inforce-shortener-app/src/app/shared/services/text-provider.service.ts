import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { TextModel } from '../models/textModel';
import { Observable } from 'rxjs/internal/Observable';
import { editWebContentModel } from '../models/editWebContentModel';

@Injectable({
  providedIn: 'root'
})
export class TextProviderService {

  private baseUrl: string = environment.baseUrl;
  
  private webContentUrl: string = this.baseUrl + '/webcontent?textName=';
  private editWebContentUrl: string = this.baseUrl + '/WebContent';

  constructor(private http: HttpClient) { }

  getText(textName: string): Observable<TextModel> {
    return this.http.get<TextModel>(this.webContentUrl + textName).pipe();
  }

  editText(textName: string, textValue: string) {
    this.http.post(this.editWebContentUrl, {textName, textValue}).pipe().subscribe();
  }
}
