import { Component, Input, OnInit, ViewEncapsulation } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { MembersService } from 'src/app/_services/members.service';
import { Member } from 'src/app/_models/member';
import { PresenceService } from 'src/app/_services/presence.service';

@Component({
  selector: 'app-member-cards',
  templateUrl: './member-cards.component.html',
  styleUrls: ['./member-cards.component.css'],
  encapsulation:ViewEncapsulation.None
})
export class MemberCardsComponent implements OnInit{
  
  @Input() member:Member|undefined;
  constructor(private memberService:MembersService,private toastr:ToastrService,
    public presenceService:PresenceService)
    {
    
  }
  ngOnInit(): void {
    
  }
    addLike(member:Member)
    {
      this.memberService.addLike(member.userName).subscribe({
        next:()=>this.toastr.success("you have liked "+member.knownAs)
      })
    }
    
}
