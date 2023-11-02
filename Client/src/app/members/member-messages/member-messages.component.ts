import { Component, Input, OnInit, ViewChild } from '@angular/core';
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
  // @Input()  messages:Message[]=[];     <!--  line we are sending messages to components but after adding message hub in aoi,message service to singalusing we are getting messages from signalr only we commented this line after messageservice connecting to message hub with signalR -->
  @ViewChild('messageForm') messageForm?:NgForm;
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

  sendMessage()
  {
    if(!this.username) return;
    this.messageService.sendMessage(this.username,this.messageContent).then(()=>{
      this.messageForm?.reset();
    })
   
    // if(!this.username) return;
    // // this.loading=true;
    // this.messageService.sendMessage(this.username,this.messageContent).subscribe({

     
      
    //  next:messages=>console.log("")
     
      // this.messages.push(messages)
      // this.messageForm.reset();
    // })
  }
}
