import { Component, Inject } from '@angular/core';
import { TaskService } from '../../shared/task.service';
import { FormBuilder } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-add-task',
  templateUrl: './add-task.component.html',
  styleUrl: './add-task.component.css'
})
export class AddTaskComponent {


  constructor(public service: TaskService, private fb: FormBuilder,
    private toastr: ToastrService,
    public dialogRef: MatDialogRef<AddTaskComponent>,
    @Inject(MAT_DIALOG_DATA) public data:any) { }



    onNoClick(): void {
      this.dialogRef.close();
    }
    
    onSubmit(): void {
      this.dialogRef.close();
    }
}
