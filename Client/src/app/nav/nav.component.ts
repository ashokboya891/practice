import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { Router } from '@angular/router';
import { Observable, of } from 'rxjs';
import { User } from '../models/User';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit{
  model:any={};
 currentUser$:Observable<User|null >=of(null);
  constructor(public accountService:AccountService,private rou:Router,private toastr:ToastrService) {
    
  }
  ngOnInit(): void {
   this.currentUser$=this.accountService.currentUser$;
  }
  login()
  {
  this.accountService.login(this.model).subscribe({
    next:_=>
      this.rou.navigateByUrl('/members'),
  //  error:error=>this.toastr.error(error.error) this toatsr funtionality is comming from interceptor after error topics concept so comented here













   
   });
 }


 logout()
 {
   this.accountService.logout();
   this.rou.navigateByUrl('/')
 
 }
}
