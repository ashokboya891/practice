import { Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs';
import { AccountService } from 'src/app/_services/account.service';
import { MembersService } from 'src/app/_services/members.service';
import { User } from 'src/app/models/User';
import { Member } from 'src/app/models/member';

@Component({
  selector: 'app-member-edit',
  templateUrl: './member-edit.component.html',
  styleUrls: ['./member-edit.component.css']
})
export class MemberEditComponent implements OnInit {
  @ViewChild('editForm')editForm:NgForm|undefined;
  @HostListener('window:beforeunload',['$event'])unloadNotification($event:any)
{
  if(this.editForm?.dirty)
  {
    $event.returnValue=true;
  }
}
  // @HostListner('window:beforeunload',['$event']) unloadNotification($event:any)
  // {
  //   if(this.editForm?.dirty)
  //   {
  //     $event.returnValue=true;
  //   }
  // }
  member:Member|undefined;
  user:User|null=null;

  constructor(private accountService:AccountService,private memberService:MembersService,private toastr:ToastrService)
   {
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next:user=>this.user=user
    })
    console.log(this.user);
    
  }
  ngOnInit(): void {
    this.loadMember();
  }
  updateMember()
  {
    // console.log(this.member);
    this.editForm?.reset(this.member);
    // this.toastr.success('profile saved sucessfully')
    this.memberService.updateMember(this.editForm?.value).subscribe({
      next:_=>{
        this.editForm?.reset(this.member);

        this.toastr.success("profile updated");
      }
    })
  }

  loadMember()
  {
    if(!this.user)return;
    this.memberService.getMember(this.user.username).subscribe({
      next:member=>this.member=member
    })
    // console.log(this.member?.age.toString);
    

  }

}
function HostListner(arg0: string): (target: MemberEditComponent, propertyKey: "editForm") => void {
  throw new Error('Function not implemented.');
}

