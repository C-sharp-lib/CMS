import {Component, ElementRef, OnInit, Renderer2} from '@angular/core';

@Component({
  selector: 'app-main-nav',
  templateUrl: './main-nav.component.html',
  styleUrls: ['./main-nav.component.css']
})
export class MainNavComponent implements OnInit {
  constructor(private el: ElementRef, private renderer: Renderer2) { }
  ngOnInit () {

  }
  newSectionUp() {
    const section = this.el.nativeElement.querySelector('#main-nav');
    this.renderer.addClass(section, 'active');
  }
}
