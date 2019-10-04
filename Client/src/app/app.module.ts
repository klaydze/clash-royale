import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';

import { MainNavbarComponent } from './home/main-navbar.component';
import { ChestsModule } from './chests/chests.module';
import { ServiceWorkerModule } from '@angular/service-worker';
import { environment } from '../environments/environment';
import { HomeComponent } from './home/home/home.component';
import { CoreModule } from './core/core.module';
import { UnauthorizedComponent } from './shared/unauthorized/unauthorized.component';
import { NgbCollapseModule } from '@ng-bootstrap/ng-bootstrap';

@NgModule({
  declarations: [
    AppComponent,
    MainNavbarComponent,
    HomeComponent,
    UnauthorizedComponent
  ],
  imports: [
    BrowserModule,
    ChestsModule,
    AppRoutingModule,
    NgbCollapseModule,
    CoreModule,
    ServiceWorkerModule.register('ngsw-worker.js', { enabled: environment.production })
  ],
  // entryComponents: [CardModalContentComponent],
  bootstrap: [AppComponent]
})
export class AppModule { }
