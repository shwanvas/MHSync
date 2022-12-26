import { HttpClient, } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Register } from './register';

@Injectable({
  providedIn: 'root'
})
export class SharedserviceService {

  baseApiUrl :String = 'https://localhost:7202/'
  constructor(private httpClient: HttpClient) { }

  getusers():Observable<Register[]>{
    console.log('getusers'+this.baseApiUrl+'getall')
    return this.httpClient.get<Register[]>(this.baseApiUrl+'getall')
  }
  updateParticipation(val:any){
    return this.httpClient.put('https://localhost:7202/updateSync',val);
  }
}
