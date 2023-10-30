import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '../_models/User';
import { BehaviorSubject, map } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
baseUrl=environment.apiUrl;
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
          this.setCurrentUser(user);

            // localStorage.setItem('user',JSON.stringify(user))

            // this.currentUserSource.next(user);
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
    user.roles=[];
    const roles=this.getDecodedToken(user.token).role;
    Array.isArray(roles)?user.roles=roles:user.roles.push(roles);
    localStorage.setItem('user',JSON.stringify(user))

    this.currentUserSource.next(user);
  }
  register(model:any)
  {
     return this.http.post<User>(this.baseUrl+ 'account/Register',model).pipe(
       map(user=>{
         if(user)
         {
           // localStorage.setItem('user',JSON.stringify(user))
           // this.currentUserSource.next(user);
             this.setCurrentUser(user);
         }
         return user;
       })
     )
    }
    getDecodedToken(token:string)
   {
    return JSON.parse(atob(token.split('.')[1]))
   }

}
