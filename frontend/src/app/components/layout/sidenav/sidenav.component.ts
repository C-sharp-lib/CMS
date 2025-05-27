import { Component, OnInit } from '@angular/core';
import {UsersService} from "../../../services";
import {BehaviorSubject} from "rxjs";
import {MenuService} from "../../../services/menu.service";

@Component({
  selector: 'app-sidenav',
  templateUrl: './sidenav.component.html',
  styleUrls: ['./sidenav.component.css']
})
export class SidenavComponent implements OnInit {
  filteredMenuItems: any[] = [];
  isExpanded = false;
  dropdowns = {
    users: false,
    jobs: false,
    home: false,
    contacts: false,
    account: false,
  }
  menuItems = [
    {icon: 'bi bi-house', key: 'home', label: 'Home', children: [
        {icon: 'bi bi-house', label: 'Home', path: '', requiresAuth: false},
        {icon: 'bi bi-info-circle', label: 'About', path: 'about', requiresAuth: false},
        {icon: 'bi bi-telephone', label: 'Contact', path: 'contact', requiresAuth: false},
        {icon: 'bi bi-shield-exclamation', label: 'Privacy Policy', path: 'privacy-policy', requiresAuth: false},
        {icon: 'bi bi-file-lock', label: 'Terms & Conditions', path: 'terms-and-conditions', requiresAuth: false},
      ]},
    {icon: 'bi bi-unlock2', label: 'Account', key: 'account', children: [
        {label: 'Login', path: 'account', icon: 'bi bi-person-lock', requiresAuth: false},
        {label: 'Register', path: 'account/register-page', icon: 'bi bi-person-add', requiresAuth: false},

      ]},
    {icon: 'bi bi-people', label: 'Users', key: 'users', children: [
        {label: 'All Users', path: 'users', icon: 'bi bi-people', requiresAuth: true},
      ]},
    {icon: 'bi bi-briefcase', label: 'Jobs', key: 'jobs', children: [
      {label: 'All Jobs', path: 'jobs', icon: 'bi bi-card-checklist', requiresAuth: true},
        {label: 'Add Job', path: 'jobs/create', icon: 'bi bi-building-add', requiresAuth: true}
      ]},
    {icon: 'bi bi-telephone', label: 'Contacts', key: 'contacts', children: [
        {label: 'All Contacts', path: 'contacts', icon: 'bi bi-card-checklist', requiresAuth: true},
        {label: 'Add Contact', path: 'contacts/create', icon: 'bi bi-building-add', requiresAuth: true}
      ]},
  ];
  constructor(private userService: UsersService, private menuService: MenuService) { }

  ngOnInit(): void {
    this.menuService.login(this);

    this.updateMenuItems();
  }
  toggleSidebar() {
    this.isExpanded = !this.isExpanded;
  }
  toggleDropdown(menu: string) {
    this.dropdowns[menu] = !this.dropdowns[menu];
  }

  updateMenuItems(): void {
    const isLoggedIn = this.userService.isLoggedIn();

    this.filteredMenuItems = this.menuItems
      .map(group => {
        const filteredChildren = group.children.filter(child =>
          (child.requiresAuth && isLoggedIn) ||
          (!child.requiresAuth && !isLoggedIn) ||
          child.requiresAuth === undefined
        );

        // Only return the group if it has visible children
        if (filteredChildren.length > 0) {
          return { ...group, children: filteredChildren };
        }

        return null;
      })
      .filter(group => group !== null);
  }
}
