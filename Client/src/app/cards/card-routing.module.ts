import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CardsComponent } from './cards.component';
import { CardResolver } from './card-resolver.service';

@NgModule({
  imports: [
    RouterModule.forChild([
      { path: '', component: CardsComponent, resolve: { resolvedCardsData: CardResolver } },
      // { path: ':id', component: CardsComponent }
    ])
  ],
  declarations: [],
  exports: [RouterModule]
})
export class CardRoutingModule { }
