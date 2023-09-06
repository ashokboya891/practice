import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit{

  model:any={};
  ngOnInit(): void {
    // throw new Error('Method not implemented.');
  }
  constructor(private accountServices:AccountService) {
    
  }
  // registerForm=false;
  registerForm:FormGroup=new FormGroup({});

  register()
  {
    // this.accountServices.Register(this.model).subscribe({
      // next:response=>{
      //   this.router.navigateByUrl('/members')
        
      // },
      // error:error=>{
      //   // this.toastr.error(error.error);
      //   // console.log(error);
      //   this.validationErrors=error
        
      // }
      
  

  }
}
