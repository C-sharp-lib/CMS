import {Component, Input, OnInit, ViewChild} from '@angular/core';
import {UsersService} from "../../../services";
import {BehaviorSubject, filter} from "rxjs";
import {MenuService} from "../../../services";
import {User} from "../../../models/user";
import {NavigationEnd, Router} from "@angular/router";

@Component({
  selector: 'app-sidenav',
  templateUrl: './sidenav.component.html',
  styleUrls: ['./sidenav.component.css']
})
export class SidenavComponent implements OnInit{
  selectedItem: any = null;
  isLoggedIn: boolean = false;
  userImageUrl: string = '';
  isLoading: boolean = true;
  defaultPhoto: string = 'assets/img/user-placeholder.png';
  fullUser: any;
  error: string = '';
  collapsed = true;
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
  constructor(private userService: UsersService, private menuService: MenuService, private router: Router) { }

  ngOnInit(): void {
    this.refreshMainMenu();
    this.menuService.login(this);
    this.loadUser();
    this.getInitials(this.fullUser?.name);
    this.router.events
      .pipe(filter(event => event instanceof NavigationEnd))
      .subscribe(() => {
        this.loadUser(); // re-load user every time navigation ends
      });

  }
  toggleCollapse() {
    this.collapsed = !this.collapsed;
  }
  refreshMainMenu(): void {
    this.menuService.refreshMenu();
  }
  get filteredMenuItems() {
    const isLoggedIn = this.userService.isLoggedIn();
    return this.menuItems
      .map(menu => ({
        ...menu,
        children: menu.children.filter(child =>
          (child.requiresAuth && isLoggedIn) ||
          (!child.requiresAuth && !isLoggedIn) ||
          child.requiresAuth === undefined
        ),
      }))
      .filter(menu => menu.children.length > 0);

  }

  getCurrentUserId(): string {
    const token = localStorage.getItem('token');
    if (!token || token.split('.').length !== 3) {
      return '';
    }

    try {
      const payload = JSON.parse(atob(token.split('.')[1]));
      return payload.nameid || '';
    } catch (e) {
      console.error('Invalid token:', e);
      return '';
    }
  }
  getCurrentUser() {
    const userId = this.getCurrentUserId();

    this.userService.getUserById(userId).subscribe({
      next: (user) => {
        console.log('Full user info:', user);
        this.fullUser = user;

      },
      error: (err) => {
        console.error('Failed to fetch user:', err);
      }
    });
  }
  isUserLoggedIn(){
    this.isLoggedIn = this.userService.isLoggedIn();
    return this.isLoggedIn;
  }
  loadUser(): void {
    const id = this.getCurrentUserId();
    this.userService.getUserById(id).subscribe({
      next: (data) => {
        this.fullUser = data;
        if (this.fullUser?.imageUrl) {
          const relativePath = `Uploads/User/${this.fullUser?.imageUrl}`;
          this.loadUserImage(relativePath);
          this.refreshMainMenu();
          this.filteredMenuItems;
          console.log(this.fullUser?.imageUrl);
        } else {
          this.userImageUrl = this.defaultPhoto;
        }
        this.isLoading = false;
      },
      error: (err) => {
        this.error = err.message;
        console.error(this.error);
        this.isLoading = false;
      }
    });
  }
  loadUserImage(relativePath: string): void {
    this.userService.getUserImageUrl(relativePath).subscribe({
      next: (response) => {
        this.refreshMainMenu();
        this.userImageUrl = response.imageUrl;
      },
      error: (err) => {
        this.userImageUrl = null;
        console.error(err.message);
      }
    });
  }
  openSubmenu(item: any) {
    this.selectedItem = this.selectedItem === item ? null : item;
  }

  closeSubmenu() {
    this.selectedItem = null;
  }

  getInitials(name: string): string {
    if (!name) return '';

    const parts = name.trim().split(' ');
    const first = parts[0]?.charAt(0) || '';
    const last = parts.length > 1 ? parts[1]?.charAt(0) : '';
    return (first + last).toUpperCase();
  }
}
