import { Component, OnInit, ViewChild ,OnDestroy} from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Member } from 'src/app/_models/member';
import { MembersService } from 'src/app/_services/members.service';
import {  CommonModule} from "@angular/common";
import { TabDirective, TabsModule, TabsetComponent } from 'ngx-bootstrap/tabs';
import { ReactiveFormsModule,FormsModule } from "@angular/forms";
import { GalleryModule, GalleryItem, ImageItem } from 'ng-gallery';
import { TimeagoModule } from "ngx-timeago";
import { MemberMessagesComponent } from '../member-messages/member-messages.component';
import { MessageService } from 'src/app/_services/message.service';
import { Message } from 'src/app/_models/message';
import { PresenceService } from 'src/app/_services/presence.service';

@Component({
  selector: 'app-member-detail',
  standalone:true,
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css'],
  imports:[CommonModule,TabsModule,ReactiveFormsModule,FormsModule,GalleryModule,TimeagoModule,MemberMessagesComponent]
  
})
export class MemberDetailComponent  implements OnInit{
  @ViewChild('memberTabs',{static:true})memberTabs?:TabsetComponent;
  activeTab?:TabDirective;
  member:Member={} as Member;  //we can keep mebers:Memers|undefined but it represents undefined if we do this in this way atleat empty obj will intialized and ts obj so no need for optional chainig ? in html code & no need of ngif in first line 
  images:GalleryItem[]=[]
  messages:Message[]=[];
  ngOnInit(): void {

    this.route.data.subscribe({
      next:data=>this.member=data['member']
    })

    // this.loadMember();
    this.route.queryParams.subscribe({
      next:params=>{
        params['tab'] &&  this.selectTab(params['tab'])
      }
    })
    this.getImages()

  }
  constructor( private mser:MembersService,private route:ActivatedRoute,
    private messageService:MessageService,public presenceService:PresenceService) {
    
  }
  onTabActivated(data:TabDirective)
  {
    this.activeTab=data;
    if(this.activeTab.heading==='Messages')
    {
      // this.m.createHubConnection(this.user,this.member.userName);
      this.loadMessages();
    }
    // else{
    // this.messageService.stopHubConnection();
    // }
  }
  loadMessages()
  {
    if(this.member)
    {
      this.messageService.getMessageThread(this.member.userName).subscribe({
        next:messages=>{
          this.messages=messages;
        }
      })
    }
  }
  selectTab(heading:string)
  {
    if(this.memberTabs)
    {
      this.memberTabs.tabs.find(x=>x.heading===heading)!.active=true;

    }
  }
  gotmem:Member|undefined;
  getMember()
  {
  
    
  }
  // this is commented after added route resolver added in message
  // loadMember()
  // {
  //   const username=this.route.snapshot.paramMap.get('username');
  //   if(!username)return;
  //   this.mser.getMember(username).subscribe({
  //     next:member=>{
  //       this.member=member
  //       // this.getImages() 
  //     }
  //   })
  // }
  getImages()
  {
    if(!this.member)return;
    for(const photo of this.member?.photos)
    {
      this.images.push(new ImageItem({src:photo.url,thumb:photo.url}));
      // this.images.push(new ImageItem({src:photo.url,thumb:photo.url}));
      // this.images.push(new ImageItem({src:photo.url,thumb:photo.url}));
    }
  }
}
