import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { User } from './models/User';
import { AccountService } from './_services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'Insta Lite';
  users:any;

  constructor(private http:HttpClient,private accountService:AccountService)
  {

  }
  ngOnInit(): void {
    this.setCurrentUser();
    this.http.get('https://localhost:5001/api/users').subscribe({
      next: (data) => {
        this.users = data;
      },
      error: (error) => {
        console.error(error);
        complete:()=>console.log("Request has done");
        
      }
      
    })
  }
  setCurrentUser()
  {
    const userString=localStorage.getItem('user');
    if(!userString) return ;
    const user:User=JSON.parse(userString);
    this.accountService.setCurrentUser(user);
  }

}
