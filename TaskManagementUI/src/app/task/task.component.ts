import { Component } from '@angular/core';
import { TaskService } from '../shared/task.service';
import { Task } from '../model/task.model';
import { Status } from '../model/status.enum';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { AddTaskComponent } from './add-task/add-task.component';
import { ToastrService } from 'ngx-toastr';


@Component({
  selector: 'app-task',
  templateUrl: './task.component.html',
  styleUrl: './task.component.css'
})
export class TaskComponent {

  tasks: Task[] = [];
  task: Task;

  PendingTasks: Task[] = [];
  InProgressTasks: Task[] = [];
  CompletedTasks: Task[] = [];


  constructor(private service: TaskService, public dialog: MatDialog, private toastr: ToastrService) {

  }

  ngOnInit(): void {
    //Called after the constructor, initializing input properties, and the first call to ngOnChanges.
    //Add 'implements OnInit' to the class.
    this.getAllTask();
  }

  getAllTask() {

    this.service.getAllTask().subscribe(
      res => {
        this.tasks = <Task[]>res;
        // console.log(res);

        // Clear existing task arrays
        this.PendingTasks = [];
        this.InProgressTasks = [];
        this.CompletedTasks = [];

        // Categorize tasks based on status
        for (const task of this.tasks) {
          switch (task.status) {
            case 'Pending':
              this.PendingTasks.push(task);
              break;
            case 'InProgress':
              this.InProgressTasks.push(task);
              break;
            case 'Completed':
              this.CompletedTasks.push(task);
              break;
          }
        }

      },
      err => {
        console.log(err);
      }
    );

  }


  moveToInProgress(id: number) {
    this.service.getTaskbyId(id).subscribe(
      res => {
        this.task = <Task>res;

        if (this.task != null) {
          this.task.status = Status.InProgress;
          this.updateStatus(id, this.task);
        }
      },
      err => {
        console.log(err);
      }
    );
  }


  moveToCompleted(id: number) {
    this.service.getTaskbyId(id).subscribe(
      res => {
        this.task = <Task>res;

        if (this.task != null) {
          this.task.status = Status.Completed;
          this.updateStatus(id, this.task);
        }
      },
      err => {
        console.log(err);
      }
    );
  }


  updateStatus(id: number, task: Task) {
    this.service.updateTask(id, task).subscribe(
      res => {
        this.getAllTask();
      },
      err => {
        console.log(err);
      }

    );
  }


  EditTask(task:any){
    this.service.populateForm(task);
   
    this.service.formTitle = "Update Task"
    this.service.buttonName = "Update"
  
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.width = "700px";
    dialogConfig.height = "500px";
    const dialogRef = this.dialog.open(AddTaskComponent, dialogConfig);
  
    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      this.ngOnInit()
    });

  }




  DeleteTask(id:number){
    let con = confirm("Are you sure to delete this one")
    if(con){
      this.service.deleteTask(id).subscribe(
        res=>{
          this.toastr.warning("Task Deleted Successfully", "Task Deleted!")
         this.getAllTask();
        },
        err=>{
          console.log(err);
        },
      )

    }
  }


//Add New Dialog

openAddNewDialog(): void {
  this.service.formTitle = "Add New Task"
  this.service.buttonName = "Save"

  const dialogConfig = new MatDialogConfig();
  dialogConfig.disableClose = true;
  dialogConfig.autoFocus = true;
  dialogConfig.width = "700px";
  dialogConfig.height = "500px";
  const dialogRef = this.dialog.open(AddTaskComponent, dialogConfig);

  dialogRef.afterClosed().subscribe(result => {
    console.log('The dialog was closed');
    this.ngOnInit()
  });

}




}
