import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { Task } from '../model/task.model';

@Injectable({
  providedIn: 'root'
})
export class TaskService {

  readonly BaseURI = environment.apiURL;
  formTitle = ""
  buttonName = ""

  constructor(private http:HttpClient) { }


  getAllTask(){
    return this.http.get(this.BaseURI + '/Task');
  }

  getTaskbyId(id:number){
    return this.http.get(this.BaseURI + '/Task/' + id);
  }

  updateTask(id:number, task:Task){
    return this.http.put(this.BaseURI + '/Task/'+id, task);
  }


}
