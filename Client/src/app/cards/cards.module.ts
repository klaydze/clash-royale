import { NgModule } from '@angular/core';
import { NgxPaginationModule } from 'ngx-pagination';
import { CardService } from './card.service';
import { CardRoutingModule } from './card-routing.module';
import { SharedModule } from '../shared/shared.module';
import { CardsComponent, CardModalContentComponent } from './card-list/cards.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { CardDetailsComponent } from './card-details/card-details.component';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  imports: [
    SharedModule,
    CardRoutingModule,
    NgxPaginationModule,
    NgbModule,
    HttpClientModule
  ],
  declarations: [
    CardsComponent,
    CardModalContentComponent,
    CardDetailsComponent
  ],
  providers: [
    CardService
  ],
  entryComponents:[
    CardModalContentComponent
  ]
})
export class CardsModule { }
