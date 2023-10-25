import { Component, OnInit } from '@angular/core';
import { Observable, take } from 'rxjs';
import { AccountService } from 'src/app/_services/account.service';
import { MembersService } from 'src/app/_services/members.service';
import { User } from 'src/app/_models/User';
import { Member } from 'src/app/_models/member';
import { Pagination } from 'src/app/_models/pagination';
import { userParams } from 'src/app/_models/userParams';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})



export class MemberListComponent implements OnInit {


  // members$:Observable<Member[]>|undefined;
  members:Member[]=[];
  pagination:Pagination | undefined;
  // pageNumber=1 ;
  // pageSize=5;
  userParams:userParams | undefined| null;
  user:User | undefined;
  // userParams:userParams;
  genderList=[{value:'male',display:'Males'},{value:'female',display:'Females'}]



  constructor(private memberService:MembersService,private accountService:AccountService) {

    this.userParams=this.memberService.getUserParams();

    //    this.accountService.currentUser$.pipe(take(1)).subscribe({
    //   next:user=>{
    //     if(user)
    //     {
    //       this.userParams=new userParams(user);
    //       this.user=user;
    //     }
    //   }
    // })
    
  }


  ngOnInit(): void {
    // this.members$=this.memberService.getMembers();
    // this.getMembers();
    this.loadmembers();
  }
 
  loadmembers()
  {
    if(!this.userParams)return;
    this.memberService.setUserParams(this.userParams);
    this.memberService.getMembers(this.userParams).subscribe({
        next:response=>{
          if(response.result && response.pagination)
          {
            this.members=response.result;
            this.pagination=response.pagination;
          }
        }
      })
    
  }
  pageChanged(event:any)
  {
    if(this.userParams && this.userParams?.pageNumber!==event.page)
    {

      this.userParams.pageNumber=event.page;
      this.memberService.setUserParams(this.userParams);
      this.loadmembers();
    }
    
  }
  resetFilters()
  {
    this.userParams=this.memberService.resetUserParams();
    this.loadmembers();
    // if(this.user)
    // {
    //   this.userParams=this.memberService.resetUserParams();
    //   this.loadmembers();
    // }
    // return;
 
  }


// commented below methods after using store method in section 10 124 like memory to store data instead of asking every time server 
  // getMembers()
  // {
  //   this.member.getMembers().subscribe({next:resp=>{
  //     this.members=resp;
  //     console.log(resp);
      
  //   }})
  // }
   

}
