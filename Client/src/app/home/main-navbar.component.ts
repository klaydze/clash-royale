import { Component } from '@angular/core';

@Component({
  selector: 'app-main-navbar',
  templateUrl: './main-navbar.component.html',
  styleUrls: ['./main-navbar.component.scss']
})
export class MainNavbarComponent {
  homeNavBarBrandTitle = 'Clash Royale Guide';
  isHomeNavbarCollapsed = true;
}
