import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import {Register} from './users/register';
@Injectable({
  providedIn: 'root'
})
export class SharedService {
  private baseApiUrl = 'https://localhost:7265/'
  constructor(private httpClient: HttpClient) { }

  addUser(register:Register) : Observable<any>{
    const headers = {'content-type':'application/json'}
    const body = JSON.stringify(register);
    console.log(body);
    return this.httpClient.post(this.baseApiUrl+'addSync',body,{'headers':headers})
  }
}
