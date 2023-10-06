import { Component, OnInit, ViewChild ,OnDestroy} from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Member } from 'src/app/models/member';
import { MembersService } from 'src/app/_services/members.service';
import {  CommonModule} from "@angular/common";
import { TabDirective, TabsModule, TabsetComponent } from 'ngx-bootstrap/tabs';
import { ReactiveFormsModule,FormsModule } from "@angular/forms";
import { GalleryModule, GalleryItem, ImageItem } from 'ng-gallery';


@Component({
  selector: 'app-member-detail',
  standalone:true,
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css'],
  imports:[CommonModule,TabsModule,ReactiveFormsModule,FormsModule,GalleryModule]
  
})
export class MemberDetailComponent  implements OnInit{
  member: Member | undefined;
  images:GalleryItem[]=[]
  ngOnInit(): void {
    this.loadMember();
  }
  constructor( private mser:MembersService,private route:ActivatedRoute) {
    
  }
   gotmem:Member|undefined;
  getMember()
  {
  
    
  }
  loadMember()
  {
    const username=this.route.snapshot.paramMap.get('username');
    if(!username)return;
    this.mser.getMember(username).subscribe({
      next:member=>{
        this.member=member,
        this.getImages()
      }
    })
  }
  getImages()
  {
    if(!this.member)return;
    for(const photo of this.member?.photos)
    {
      this.images.push(new ImageItem({src:photo.url,thumb:photo.url}));
      this.images.push(new ImageItem({src:photo.url,thumb:photo.url}));
      this.images.push(new ImageItem({src:photo.url,thumb:photo.url}));
    }
  }
}
