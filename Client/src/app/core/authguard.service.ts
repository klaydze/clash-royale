import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthguardService implements CanActivate {

  constructor(private _authService: AuthService,
    private _router: Router) { }

  canActivate(route: import("@angular/router").ActivatedRouteSnapshot, state: import("@angular/router").RouterStateSnapshot): boolean | import("@angular/router").UrlTree | import("rxjs").Observable<boolean | import("@angular/router").UrlTree> | Promise<boolean | import("@angular/router").UrlTree> {
    if (this._authService.isLoggedIn()) { 
      return true; 
    }

    // this._router.navigate(['/login'], { queryParams: { redirect: state.url }, replaceUrl: true });
    this._router.navigate(['/unauthorized']);
    
    return false;
  }
}
