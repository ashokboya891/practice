import { Component, Input, OnInit } from '@angular/core';
import { Message } from 'src/app/_models/message';
import { MessageService } from 'src/app/_services/message.service';
import { CommonModule } from '@angular/common';
import { FormsModule, NgForm } from '@angular/forms';
import { TimeagoModule } from "ngx-timeago";

@Component({
  selector: 'app-member-messages',
  standalone:true,
  templateUrl: './member-messages.component.html',
  styleUrls: ['./member-messages.component.css'],
  imports:[CommonModule,FormsModule,TimeagoModule]
})
export class MemberMessagesComponent  implements OnInit{
  @Input() username?:string;
  @Input()  messages:Message[]=[]; 
 messageContent='';

  constructor(public messageService:MessageService) {
    
  }
  ngOnInit(): void {
    // this.loadMessages();
  }
  // loadMessages()
  // {
  //   if(this.username)
  //   {
  //     this.messageService.getMessageThread(this.username).subscribe({
  //       next:messages=>{
  //         this.messages=messages;
  //       }
  //     })
  //   }
  // }
}
