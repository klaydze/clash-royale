import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
// import { CardsModule } from './cards/cards.module';
import { AppRoutingModule } from './app-routing.module';
// import { ArenasModule } from './arenas/arenas.module';
// import { CardModalContentComponent } from './cards/cards.component';
import { MainNavbarComponent } from './home/main-navbar.component';
import { ChestsModule } from './chests/chests.module';
import { ServiceWorkerModule } from '@angular/service-worker';
import { environment } from '../environments/environment';
import { HomeComponent } from './home/home/home.component';

@NgModule({
  declarations: [
    AppComponent,
    MainNavbarComponent,
    HomeComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    // CardsModule,
    // ArenasModule,
    ChestsModule,
    AppRoutingModule,
    NgbModule,
    ServiceWorkerModule.register('ngsw-worker.js', { enabled: environment.production })
  ],
  // entryComponents: [CardModalContentComponent],
  bootstrap: [AppComponent]
})
export class AppModule { }
