import {Injectable, OnInit} from '@angular/core';
import {SidenavComponent} from "../components/layout";

@Injectable({
  providedIn: 'root'
})
export class MenuService {
  private sideNavComponent?: SidenavComponent;
  constructor() { }
  login(component: SidenavComponent) {
    this.sideNavComponent = component;
  }

  refreshMenu() {
    this.sideNavComponent?.filteredMenuItems;
  }
}
