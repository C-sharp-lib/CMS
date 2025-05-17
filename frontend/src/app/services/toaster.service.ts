import { Injectable } from '@angular/core';
import {ToastrService} from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class ToasterService {

  constructor(private toastr: ToastrService) { }

  showErrorToast(message: string, content: string): void {
    this.toastr.error(message, content);
  }
  showSuccessToast(message: string, content: string): void {
    this.toastr.success(message, content);
  }
  showInfoToast(message: string, content: string): void {
    this.toastr.info(message, content);
  }
  showWarningToast(message: string, content: string): void {
    this.toastr.warning(message, content);
  }

}
