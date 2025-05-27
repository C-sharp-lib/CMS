import {CanActivateFn, Router} from '@angular/router';
import {inject} from "@angular/core";
import {UsersService} from "../../services";

export const authGuard: CanActivateFn = (route, state) => {
  const userService = inject(UsersService);
  const router = inject(Router);
  if(userService.isLoggedIn()) {
    return true;
  } else {
    router.navigate(['/account']);
    return false;
  }
}
