import { Injectable } from '@angular/core';
import {BehaviorSubject} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class BreadcrumbService {
  private breadcrumbLabel = new BehaviorSubject<string>('');
  breadcrumb$ = this.breadcrumbLabel.asObservable();
  constructor() { }

  setBreadcrumb(label: string) {
    this.breadcrumbLabel.next(label);
  }
}
