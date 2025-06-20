import {Component, HostListener, OnInit} from '@angular/core';
import {FormControl} from "@angular/forms";
import {SearchService} from "../../../services";
import {Router} from "@angular/router";
import {debounceTime, map, of, switchMap} from "rxjs";

@Component({
  selector: 'app-search-bar',
  templateUrl: './search-bar.component.html',
  styleUrls: ['./search-bar.component.css']
})
export class SearchBarComponent implements OnInit {
  searchControl = new FormControl('');
  filteredResults: any[] = [];
  showResults: boolean = false;
  isVisible: boolean = false;
  showOverlay = false;
  private siteData = [
    {title: 'Home', route: '/', type: 'page'},
    {title: 'About', route: '/about', type: 'page'},
    {title: 'Contact', route: '/contact', type: 'page'},
    {title: 'Privacy Policy', route: '/privacy-policy', type: 'page'},
    {title: 'Terms and Conditions', route: '/terms-and-conditions', type: 'page'},
    {title: 'Login', route: '/account', type: 'page'},
    {title: 'Register', route: '/account/register-page', type: 'page'},
  ];
  constructor(private searchService: SearchService, private router: Router) {
  }
  ngOnInit() {
    this.searchControl.valueChanges
      .pipe(
        debounceTime(300),
        switchMap(query => {
          if (!query) return of([]);
          const lower = query.toLowerCase();
          const staticMatches = this.siteData.filter(item =>
            item.title.toLowerCase().includes(lower)
          );
          return this.searchService.search(query).pipe(
            map(userResults => [...staticMatches, ...userResults])
          );
        })
      )
      .subscribe(results => {
        this.filteredResults = results;
      });
  }

  openOverlay() {
    this.showOverlay = true;
    setTimeout(() => document.getElementById('overlayInput')?.focus(), 0);
  }

  closeOverlay() {
    this.showOverlay = false;
    this.searchControl.setValue('');
    this.filteredResults = [];
  }

  goTo(route: string) {
    this.router.navigate([route]);
    this.closeOverlay();
  }

  @HostListener('window:keydown.escape')
  onEscape() {
    if (this.showOverlay) this.closeOverlay();
  }
}
