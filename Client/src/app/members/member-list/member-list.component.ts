import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { MembersService } from 'src/app/_services/members.service';
import { Member } from 'src/app/models/member';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent implements OnInit {
  members$:Observable<Member[]>|undefined;

  ngOnInit(): void {
    this.members$=this.memberService.getMembers();
    // this.getMembers();
  }

constructor(private memberService:MembersService) {
  
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
