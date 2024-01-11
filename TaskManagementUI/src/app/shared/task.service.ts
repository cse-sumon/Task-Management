import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { DropdownList, Task } from '../model/task.model';
import { FormBuilder, Validators } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class TaskService {

  readonly BaseURI = environment.apiURL;
  formTitle = ""
  buttonName = ""



  constructor(private http:HttpClient, private fb: FormBuilder) { }

  taskForm = this.fb.group({
    id: [null],
    title: ['', Validators.required],
    dueDate: [null, Validators.required],
    priority: ['', Validators.required],
    status: ['', Validators.required],
    description: [''],
    createdBy: [''],
    createdByName: [''],
    assignedTo: ['', Validators.required],
    assignedToName: ['']
  });



  getAllTask(){
    return this.http.get(this.BaseURI + '/Task');
  }

  getTaskbyId(id:number){
    return this.http.get(this.BaseURI + '/Task/' + id);
  }

  AddTask(task:any){
    return this.http.post(this.BaseURI + '/Task', task);
  }


  updateTask(id:number, task:Task){
    return this.http.put(this.BaseURI + '/Task/'+id, task);
  }


}
