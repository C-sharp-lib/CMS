import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {query} from "@angular/animations";
import {forkJoin, map, Observable, of} from "rxjs";
import {environment} from "../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class SearchService {
  private userUrl = `${environment.apiUrl}/Identity/User`;

  constructor(private http: HttpClient) { }
  private siteIndex = [
    {title: 'Home', route: '/', type: 'page'},
    {title: 'About', route: '/about', type: 'page'},
    {title: 'Contact', route: '/contact', type: 'page'},
    {title: 'Privacy Policy', route: '/privacy-policy', type: 'page'},
    {title: 'Terms and Conditions', route: '/terms-and-conditions', type: 'page'},
    {title: 'Login', route: '/account', type: 'page'},
    {title: 'Register', route: '/account/register-page', type: 'page'},
  ];
  search(query: string): Observable<any[]>{
    const lower = query.toLowerCase();
    const staticResults =  this.siteIndex.filter(item => {
      item.title.toLowerCase().includes(lower);
    });
    const userSearchUrl = `${this.userUrl}/search?query=${encodeURIComponent(query)}`;
    const userResults$= this.http
      .get<any[]>(userSearchUrl)
      .pipe(
        map(users =>
          users.map(user => ({
            title: user.name,
            route: `users/${user.id}`,
            type: 'user'
          }))
        )
      );
    console.log(userResults$);
    return forkJoin([of(staticResults), userResults$]).pipe(
      map(([routes, users]) => [...routes, ...users])
    );
  }


}
