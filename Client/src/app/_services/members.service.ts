import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Member } from '../models/member';
import { map, of, take } from 'rxjs';
import { PaginatedResult } from '../models/pagination';
import { userParams } from '../models/userParams';
import { AccountService } from './account.service';
import { User } from '../models/User';

@Injectable({
  providedIn: 'root'
})
export class MembersService {
members:Member[]=[];
baseUrl=environment.apiUrl;
memberCache=new Map();
user:User|undefined;
userParams:userParams|undefined;
// paginatedResult:PaginatedResult<Member[]>=new PaginatedResult<Member[]>

  constructor(private http:HttpClient,private accountService:AccountService)
   { 
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next:user=>{
        if(user)
        {
          this.userParams=new userParams(user);
          this.user=user;
        }
      }
    })
   }
   getUserParams()
   {
     return this.userParams;
   }
   setUserParams(params:userParams)
   {
     // if(params.pageNumber ===this.userParams?.pageNumber) params.pageNumber=1;
    this.userParams=params;
   }

  getMembers(userParams:userParams)
  {
    const response=this.memberCache.get(Object.values(userParams).join('-'))
    if(response) return of(response);

    //  console.log(Object.values(userParams).join('-'));
     
    let params = this.getPaginationHeaders(userParams.pageNumber,userParams.pageSize);
    params=params.append('minAge',userParams.minAge);
    params=params.append('maxAge',userParams.maxAge);
    params=params.append('gender',userParams.gender);
    params=params.append('orderBy',userParams.orderBy);



    return this.getPaginatedResult<Member[]>(this.baseUrl+'users',params).pipe(
      map(response=>{
        this.memberCache.set(Object.values(userParams).join('-'),response);
        return response;
      })
    )
  }


  private getPaginatedResult<T>(url:string,params: HttpParams) {

    const paginatedResult:PaginatedResult<T>=new PaginatedResult<T>;

    return this.http.get<T>(url, { observe: 'response', params }).pipe(
      map(response => {
        if (response.body) {
          paginatedResult.result = response.body;
        }
        const pagination = response.headers.get('Pagination');
        if (pagination) {
          paginatedResult.pagination = JSON.parse(pagination);
        }
        return paginatedResult;
      })

    );
  }
  

  private getPaginationHeaders(pageNumber:number,pageSize:number) {

    let params = new HttpParams();
   
      params = params.append('pageNumber', pageNumber);
      params = params.append('pageSize', pageSize);
    
    return params;
  }
  resetUserParams()
  {
    
    if(this.user)
    {
      this.userParams=new userParams(this.user);
      return this.userParams;
      console.log('in reset');

    }
    return null;
  }
    getMember(username:string)
    {
      const member=[...this.memberCache.values()].reduce((arr,elem)=>arr.concat(elem.result),[])
      .find((member:Member)=>member.userName===username);
      if(member)return of(member);
            console.log(member);

      // const member=this.members.find(x=>x.userName===username);
      // if(member)return of(member);
      return this.http.get<Member>(this.baseUrl+ 'users/'+username);

    }
    setMainPhoto(photoId:number)
    {
      return this.http.put(this.baseUrl+'users/set-main-photo/'+photoId,{});
    }
    DeletePhoto(photoId:number)
    {
      return this.http.delete(this.baseUrl+'users/delete-photo/'+photoId);

    }
    addLike(username:string)
    {
      return this.http.post(this.baseUrl+'likes/'+username,{});

    }
    getLikes(predicate:string,pageNumber:number,pageSize:number)
    {
      let params=this.getPaginationHeaders(pageNumber,pageSize);
      params=params.append('predicate',predicate);
      return this.getPaginatedResult<Member[]>(this.baseUrl+'likes',params);
      // return this.http.get<Member[]>(this.baseUrl+'likes?predicate='+predicate,{});

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
