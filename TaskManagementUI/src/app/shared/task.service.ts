import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class TaskService {

  readonly BaseURI = environment.apiURL;

  constructor(private http:HttpClient) { }


  getAllTask(){
    return this.http.get(this.BaseURI + '/Task');
  }
}
