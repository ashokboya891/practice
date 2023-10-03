import { Component, Input, OnInit, ViewEncapsulation } from '@angular/core';
import { Member } from 'src/app/models/member';

@Component({
  selector: 'app-member-cards',
  templateUrl: './member-cards.component.html',
  styleUrls: ['./member-cards.component.css'],
  encapsulation:ViewEncapsulation.None
})
export class MemberCardsComponent implements OnInit{
  @Input() member:Member|undefined;
  ngOnInit(): void {
    
  }
  
}
