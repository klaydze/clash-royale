import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CardsComponent } from './card-list/cards.component';
import { CardResolver } from './card-list/card-resolver.service';
import { CardDetailsComponent } from './card-details/card-details.component';
import { CardDetailsResolver } from './card-details/card-details-resolver.service';

@NgModule({
  imports: [
    RouterModule.forChild([
    { path: '', component: CardsComponent /*, resolve: { resolvedCardsData: CardResolver }*/ },
      { path: ':id', component: CardDetailsComponent /*, resolve: { resolvedSelectedCardData: CardDetailsResolver }*/ }
    ])
  ],
  declarations: [],
  exports: [RouterModule]
})
export class CardRoutingModule { }
