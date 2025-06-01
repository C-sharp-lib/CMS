import {Component, ElementRef, OnInit, Renderer2} from '@angular/core';
import {UsersService} from "../../../services";
import {Router} from "@angular/router";
import {MenuService} from "../../../services/menu.service";

@Component({
  selector: 'app-main-nav',
  templateUrl: './main-nav.component.html',
  styleUrls: ['./main-nav.component.css']
})
export class MainNavComponent implements OnInit {
  constructor(private el: ElementRef, private renderer: Renderer2, private usersService: UsersService,
              private router: Router, private menuService: MenuService) { }
  ngOnInit () {

  }
  newSectionUp() {
    const section = this.el.nativeElement.querySelector('#main-nav');
    this.renderer.addClass(section, 'active');
  }
  isLoggedIn() {
    return this.usersService.isLoggedIn();
  }
  logoutUser(): void {
    this.usersService.logout();
    this.menuService.refreshMenu();
    this.router.navigate(['/account']);
  }

}
