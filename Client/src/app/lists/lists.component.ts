import { Component, OnInit } from '@angular/core';
import { Member } from '../models/member';
import { MembersService } from '../_services/members.service';
import { Pagination } from '../models/pagination';
import { userParams } from '../models/userParams';

@Component({
  selector: 'app-lists',
  templateUrl: './lists.component.html',
  styleUrls: ['./lists.component.css']
})
export class ListsComponent  implements OnInit{
  members:Member[]|undefined;
  predicate='liked';
  pageNumber=1;
  pageSize=5;
  pagination:Pagination|undefined;
  // userParams:userParams | undefined;
  constructor(private memberService:MembersService) {
    
  }
  
    ngOnInit(): void {
      this.loadLikes();
    }
   
  
  loadLikes()
  {
    // this.memberService.getLikes(this.predicate).subscribe({
    //   next:response=>{
    //     this.members=response
    //   }
    // })
    this.memberService.getLikes(this.predicate,this.pageNumber,this.pageSize).subscribe({
      next:response=>{
        this.members=response.result;
        this.pagination=response.pagination;
      }
    })
  }
    pageChanged(event:any)
    {
      if(this.pageNumber!==event.page)
      {
  
        this.pageNumber!==event.page;
        this.pageNumber=event.page;
  
        this.loadLikes();
      }
  
    }
    // pageChanged(event:any)
    // {
    //   if(this.userParams?.pageNumber!==event.page)
    //   {
  
    //     this.userParams.pageNumber=event.page;
    //     // this.memberService.setUserParams(this.userParams);
    //     this.loadLikes();
    //   }
  
    // }
    // pageChanged(event:any)
    // {
    //   if( this.pageNumber!==event.page)
    //   {
  
    //     this.pageNumber=event.page;
    //     // this.memberService.setUserParams(this.userParams);
    //     this.loadLikes();
    //   }
      
    // }
  
}
