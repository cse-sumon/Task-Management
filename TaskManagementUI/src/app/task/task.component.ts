import { Component } from '@angular/core';
import { TaskService } from '../shared/task.service';

@Component({
  selector: 'app-task',
  templateUrl: './task.component.html',
  styleUrl: './task.component.css'
})
export class TaskComponent {

  tasks:any=[];
  constructor(private service: TaskService){}

  ngOnInit(): void {
    //Called after the constructor, initializing input properties, and the first call to ngOnChanges.
    //Add 'implements OnInit' to the class.
    console.log(localStorage.getItem('token'));
  this.getAllTask();
  }

  getAllTask(){

    this.service.getAllTask().subscribe(
      res=>{
        this.tasks = res;
        console.log(res);

      },
      err=>{
        console.log(err);
      }
    );

  }

}
