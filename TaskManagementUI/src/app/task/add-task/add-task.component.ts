import { Component, Inject } from '@angular/core';
import { TaskService } from '../../shared/task.service';
import { FormBuilder } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { DropdownList, Task } from '../../model/task.model';
import { UserService } from '../../shared/user.service';

@Component({
  selector: 'app-add-task',
  templateUrl: './add-task.component.html',
  styleUrl: './add-task.component.css'
})
export class AddTaskComponent {


  constructor(public service: TaskService, private fb: FormBuilder,
    private toastr: ToastrService,
    public dialogRef: MatDialogRef<AddTaskComponent>,
    @Inject(MAT_DIALOG_DATA) public data:any,
    private userService: UserService    ){ }



    statusList: DropdownList[] = [
      new DropdownList("Pending", "Pending"),
      new DropdownList("InProgress", "InProgress"),
      new DropdownList("Completed", "Completed")
    ];
  
  
    priorityList: DropdownList[] = [
      new DropdownList("Low", "Low"),
      new DropdownList("Medium", "Medium"),
      new DropdownList("High", "High")
    ];

    userList: any[]=[];
  
  


ngOnInit(): void {
  //Called after the constructor, initializing input properties, and the first call to ngOnChanges.
  //Add 'implements OnInit' to the class.
  this.getAllUsers();
}


    onNoClick(): void {
      this.dialogRef.close();
    }
    
    onSubmit(): void {
      if (this.service.taskForm.valid) {
        //const formValues = this.service.taskForm.value;
        // console.log('Submitted Form Values:', formValues);

        let task = {
          title: this.service.taskForm.get('title').value,
          dueDate: this.service.taskForm.get('dueDate').value,
          priority: this.service.taskForm.get('priority').value,
          status: this.service.taskForm.get('status').value,
          assignedTo: this.service.taskForm.get('assignedTo').value,
          description: this.service.taskForm.get('description').value,
         }
        //  console.log(task);
       
        let id = this.service.taskForm.get('id').value;
        if(id == null || id == ""){
          this.InsertTask(task);
        }
        else{
          id = this.service.taskForm.get('id').value;
          let task = {
            id: this.service.taskForm.get('id').value,
            title: this.service.taskForm.get('title').value,
            dueDate: this.service.taskForm.get('dueDate').value,
            priority: this.service.taskForm.get('priority').value,
            status: this.service.taskForm.get('status').value,
            assignedTo: this.service.taskForm.get('assignedTo').value,
            description: this.service.taskForm.get('description').value,
            assignedToName: this.service.taskForm.get('assignedToName').value,
            createdByName: this.service.taskForm.get('createdByName').value,
            createdBy: this.service.taskForm.get('createdBy').value,
           }

           this.UpdateTask(id,task);
        }
  
        
      } else {
        console.error('Form is invalid. Please check the fields.');
      }
    }


    InsertTask(task:any){
      this.service.AddTask(task).subscribe(
        res=>{
          this.toastr.success("New Task created!", "Task Added Successfully");
          this.service.taskForm.reset();
          this.dialogRef.close();
          this.ngOnInit();
        },
        err=>{
          console.log(err);
        },
      )
    }


    UpdateTask(id:number, task:any){
      this.service.updateTask(id, task).subscribe(
        res=>{
          this.toastr.success("Task Updated!", "Task Updated Successfully");
          this.service.taskForm.reset();
          this.dialogRef.close();
          this.ngOnInit();
        },
        err=>{
          console.log(err);
        },
      )
    }

    getAllUsers(){
      this.userService.getAllUsers().subscribe(
        res=>{
          // console.log(res);
          this.userList = <any>res;
        },
        err=>{
          console.log(err);
        },
      )
    }
}
