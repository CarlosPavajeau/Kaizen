import { Component, OnInit } from '@angular/core';
import { bounceInLeftOnEnterAnimation, bounceInOnEnterAnimation, bounceInUpOnEnterAnimation } from 'angular-animations';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: [ './header.component.scss' ],
  animations: [ bounceInLeftOnEnterAnimation(), bounceInOnEnterAnimation(), bounceInUpOnEnterAnimation() ]
})
export class HeaderComponent implements OnInit {
  constructor() {}

  ngOnInit(): void {}
}
