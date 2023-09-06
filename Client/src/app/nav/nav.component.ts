import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { Router } from '@angular/router';
import { Observable, of } from 'rxjs';
import { User } from '../models/User';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit{
  model:any={};
 currentUser$:Observable<User|null >=of(null);
  constructor(public accountService:AccountService,private rou:Router) {
    
  }
  ngOnInit(): void {
   this.currentUser$=this.accountService.currentUser$;
  }
  login()
  {
  this.accountService.login(this.model).subscribe({
    next:res=>
    {
      console.log(res);
    },
   error:error=>console.log(error)
   
   });
 }


 logout()
 {
   this.accountService.logout();
  //  this.router.navigateByUrl('/')
 
 }
}
