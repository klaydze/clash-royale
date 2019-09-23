import { Component, OnInit } from '@angular/core';
import { AuthService } from '../core/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-main-navbar',
  templateUrl: './main-navbar.component.html',
  styleUrls: ['./main-navbar.component.scss']
})
export class MainNavbarComponent implements OnInit {

  homeNavBarBrandTitle = 'Clash Royale Guide';
  isHomeNavbarCollapsed = true;

  constructor(private _authService: AuthService,
    private _router: Router) { }

  ngOnInit(): void {
    if (window.location.href.indexOf('?postLogout=true') > 0) {
      this._authService.signoutRedirectCallback().then(() => {
        let url: string = this._router.url.substring(0,this._router.url.indexOf('?'));
        this._router.navigateByUrl(url);
      });
    }
  }

  login() {
    this._authService.login();
  }

  logout() {
    this._authService.logout();
  }

  isLoggedIn() {
    return this._authService.isLoggedIn();
  }

  isAdmin() {
    return this._authService.authContext &&
      this._authService.authContext.claims &&
      (this._authService.authContext.claims.find(c => c.type === 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role' && c.value === 'Admin'));
  }
}
