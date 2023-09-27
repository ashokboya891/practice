import { Component, EventEmitter, Input, OnInit ,Output} from '@angular/core';
import { FormGroup } from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit{
  model:any={}
  // @Input() usersFromHomeComponent:any;   //getting input from home component 
  @Output() cancelRegister=new EventEmitter();  //sending data from register -> home for register
  ngOnInit(): void {

  }
constructor(private accountservices:AccountService,private toastr:ToastrService) {
  
}
cancel()
{
  // console.log("canceled");
  this.cancelRegister.emit(false);
  
}
register()
  {
  
    
    this.accountservices.register(this.model).subscribe({
      next:response=>{
        // console.log(response);
        this.toastr.success("registed");
        // this.cancel();
        

      },
      error:error=>this.toastr.error(error.error)
      
    });
    
  }
}
