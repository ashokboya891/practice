import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { Router } from '@angular/router';
import { Observable, of, take, takeLast } from 'rxjs';
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
 user:any;
  constructor(public accountService:AccountService,private rou:Router,private toastr:ToastrService) {
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next:user=>this.user=user
    })
    
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
