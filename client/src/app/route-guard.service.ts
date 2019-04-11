import { Injectable } from '@angular/core';

import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from "@angular/router";

@Injectable()
export class RouteGuardService implements CanActivate {

 constructor(private router: Router) {}
    public canActivate(activatedRouteSnapshot: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
      const userId = localStorage.getItem('userId');
      if (state.url === '/') {
        if (userId == null) {
          return true;
        }
        this.router.navigate(['dashboard']);
        return false;
      }
      if (userId == null) {
        this.router.navigate(['']);
        return false;
      }
      return true;
    }
}
