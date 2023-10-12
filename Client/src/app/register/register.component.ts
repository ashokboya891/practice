import { Component, EventEmitter, Input, OnInit ,Output} from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';
import { Route, Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit{
  model:any={}
  // @Input() usersFromHomeComponent:any;   //getting input from home component 
  @Output() cancelRegister=new EventEmitter();  //sending data from register -> home for register
  registerForm:FormGroup=new FormGroup({});
  maxDate:Date=new Date();
  validationErrors:string [] |undefined;
  ngOnInit(): void {
    this.initializerForm();

    this.maxDate.setFullYear(this.maxDate.getFullYear()-18);
  }

  
constructor(private accountservices:AccountService,private toastr:ToastrService,private fb:FormBuilder,private router:Router) {
}
initializerForm()
{
  this.registerForm=this.fb.group({
    gender:['male'],
    username:['',Validators.required],
    KnownAs:['',Validators.required],
    dateOfBirth:['',Validators.required],
    city:['',Validators.required],
    country:['',Validators.required],
    // username:['',Validators.required],
    password:['',[Validators.required,Validators.minLength(4),Validators.maxLength(8)]],
    confirmPassword:['',[Validators.required,this.matchvalues('password')]],
  });
  this.registerForm.controls['password'].valueChanges.subscribe({
    next:()=>this.registerForm.controls['confirmPassword'].updateValueAndValidity()
  }) 
}
matchvalues(matchTo:string):ValidatorFn
{
  return (control:AbstractControl)=>{
    return control.value===control.parent?.get(matchTo)?.value?null:{notmaching:true}
  }
}
cancel()
{
  this.cancelRegister.emit(false);
}
register()
  {
    
   const dob=this.getDateonly(this.registerForm.controls['dateOfBirth'].value);
   const values={...this.registerForm.value,dateOfBirth:dob};
   
    // console.log(values);
    
    this.accountservices.register(values).subscribe({
      next:()=>{
        this.router.navigateByUrl('/members')
        
      },
      error:error=>{
        // this.toastr.error(error.error);
        // console.log(error);
        this.validationErrors=error
        
      }
      
    });
    // console.log(this.registerForm?.value);
    
    //  this.accountservices.register(this.model).subscribe({
    //   next:response=>{
    //     this.toastr.success("registed");
    //   },
    //   error:error=>this.toastr.error(error.error)
    // });
  }
  private getDateonly(dob:string|undefined)
  {
    if(!dob)return {};
    let thedob=new Date(dob);
    return new Date(thedob.setMinutes(thedob.getMinutes()-thedob.getTimezoneOffset())).toISOString().slice(0,10);
  }

}
