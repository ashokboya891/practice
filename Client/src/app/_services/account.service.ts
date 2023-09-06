import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '../models/User';
import { BehaviorSubject, map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
baseUrl='https://localhost:5001/api/'
user:any;
private currentUserSource=new BehaviorSubject<User | null>(null);
currentUser$=this.currentUserSource.asObservable();
  constructor(private http:HttpClient) 
  { 

  }
  login(model:any)
  {
    return   this.http.post<User>(this.baseUrl+ 'account/login',model).pipe(
      map((response:User)=>{
        const user=response;
        if(user)
        {
            localStorage.setItem('user',JSON.stringify(user))
            this.currentUserSource.next(user);
        }
      })
    )

  }
  logout()
  {
    localStorage.removeItem('user')
    this.currentUserSource.next(null);

  }
  setCurrentUser(user:User)
  {
    this.currentUserSource.next(user);

  }
  // GetUsers()
  // {
  //   return this.http.get<User>(this.baseUrl+'users');
  // }
  Register(model:any)
  {
    this.http.post<User>(this.baseUrl+'account/register',model)
    .pipe(
      map((response:User)=>{
        const user=response;
        if(user)
        {
            localStorage.setItem('user',JSON.stringify(user))
            // this.currentUserSource.next(user);
        }
      })
    )
  }
}
