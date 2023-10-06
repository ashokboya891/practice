import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Member } from '../models/member';
import { map, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MembersService {
  members:Member[]=[];
baseUrl=environment.apiUrl;
  constructor(private http:HttpClient) { }
  getMembers()
  {
    if(this.members.length>0) return of(this.members);
    return this.http.get<Member[]>(this.baseUrl+'users').pipe(
      map(members=>{
        this.members=members;
        return members;
      })
    )
  }


    getMember(username:string)
    {
      const member=this.members.find(x=>x.userName===username);
      if(member)return of(member);
      return this.http.get<Member>(this.baseUrl+ 'users/'+username);

    }
    
    updateMember(member:Member)
    {
      return this.http.put(this.baseUrl+'users',member).pipe(
        map(()=>{
          const index=this.members.indexOf(member);
          this.members[index]={...this.members[index],...member}
        })
      )
    //  return this.http.put(this.baseUrl+'users',member).pipe(
    //   map(()=>{
    //     const index=this.members.indexOf(member);
    //     this.members[index]={...this.members[index],...member}
    //   })
    //  )
    }
    // this got removed after adding jwt interceptor in app.modules.ts and interrceptors foleder
    // getHttpOptions()
    // {
    //   const userString=localStorage.getItem('user');
    //   if(!userString) return ;
    //   const user=JSON.parse(userString);
    //   return{
    //     headers:new HttpHeaders({
    //       Authorization:'Bearer '+user.token
    //     })
    //     }
      
    // }

}
