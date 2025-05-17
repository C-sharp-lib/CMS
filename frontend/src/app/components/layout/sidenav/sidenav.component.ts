import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-sidenav',
  templateUrl: './sidenav.component.html',
  styleUrls: ['./sidenav.component.css']
})
export class SidenavComponent implements OnInit {
  isExpanded = false;
  dropdowns = {
    users: false,
    jobs: false,
    home: false,
  }
  menuItems = [
    {icon: 'bi bi-house', key: 'home', label: 'Home', children: [
        {icon: 'bi bi-house', label: 'Home', path: ''},
        {icon: 'bi bi-info-circle', label: 'About', path: 'about'},
        {icon: 'bi bi-telephone', label: 'Contact', path: 'contact'},
        {icon: 'bi bi-shield-exclamation', label: 'Privacy Policy', path: 'privacy-policy'},
        {icon: 'bi bi-file-lock', label: 'Terms & Conditions', path: 'terms-and-conditions'},
      ]},
    {icon: 'bi bi-people', label: 'Users', key: 'users', children: [
        {label: 'All Users', path: 'users', icon: 'bi bi-people'},
        {label: 'Register', path: 'users/register-page', icon: 'bi bi-person-add'},
        {label: 'Login', path: 'users/login-page', icon: 'bi bi-person-lock'},
      ]},
    {icon: 'bi bi-briefcase', label: 'Jobs', key: 'jobs', children: [
      {label: 'All Jobs', path: 'jobs', icon: 'bi bi-card-checklist'},
        {label: 'Add Job', path: 'jobs/create', icon: 'bi bi-building-add'}
      ]},
  ];
  constructor() { }

  ngOnInit(): void {
  }
  toggleSidebar() {
    this.isExpanded = !this.isExpanded;
  }
  toggleDropdown(menu: string) {
    this.dropdowns[menu] = !this.dropdowns[menu];
  }
}
