import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Observable, of } from 'rxjs';
import { User } from '../models/User';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit{
  currentUser$:Observable<User|null >=of(null);
  registerMode=false;
  users:any;

  ngOnInit(): void {
  }

  registerToggle() {
    this.registerMode = !this.registerMode;
  }

  cancelRegisterMode(event: boolean) {
    this.registerMode = event;
  }
  // ngOnInit(): void {
  //   this.getUsers();
  // }
  // registerToggle()
  // {
  //   this.registerMode=!this.registerMode;
  // }
  // getUsers()
  // {
  //   this.http.get('https://localhost:5001/api/users').subscribe({
  //     next:res=>{
  //       this.users=res
  //       console.log("request done");
        
  //     },
  //     error:error=>{
  //     console.log(error);
  //     }
  //   })
  // }
  // cancelRegisterMode(Event:boolean)
  // {
  //   this.registerMode=Event;
  // }

}
