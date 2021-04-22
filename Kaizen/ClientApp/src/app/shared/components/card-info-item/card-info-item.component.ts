import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-card-info-item',
  templateUrl: './card-info-item.component.html',
  styleUrls: ['./card-info-item.component.scss']
})
export class CardInfoItemComponent implements OnInit {
  @Input() title: string;
  @Input() icon: string;

  constructor() { }

  ngOnInit(): void {
  }

}
