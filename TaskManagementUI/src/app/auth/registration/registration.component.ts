import { Component } from '@angular/core';
import { UserService } from '../../shared/user.service';
import { ToastrService } from 'ngx-toastr';
import { Route, Router } from '@angular/router';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrl: './registration.component.css'
})
export class RegistrationComponent {

  constructor(public service: UserService, private toastr: ToastrService, private router : Router) { }

  ngOnInit() {
    this.service.formModel.reset();
  }


  onSubmit() {
    this.service.register().subscribe(
      (res: any) => {
        this.service.formModel.reset();
        this.toastr.success('New user created!', 'Registration successful.');
        this.router.navigateByUrl('/login');

        // if (res.status == 200) {
        //   this.service.formModel.reset();
        //   this.toastr.success('New user created!', 'Registration successful.');
        // } 
        // else {

        //   for (let i = 0; i < res.errors.length; i++) {
        //     const element = res.errors[i];
        //     switch (element.code) {
        //       case 'DuplicateUserName':
        //         this.toastr.error('Email is already taken', 'Registration failed.');
        //         break;
  
        //       default:
        //         this.toastr.error(element.description, 'Registration failed.');
        //         break;
        //     }
        //   }
        // }
        
      },
      err => {
        console.log(err);
      }
    );
  }
  
}
