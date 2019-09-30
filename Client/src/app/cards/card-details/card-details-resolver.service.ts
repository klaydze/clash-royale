import { Injectable } from "@angular/core";
import { Resolve } from "@angular/router";
import { Card } from "../card";
import { CardService } from "../card.service";

@Injectable({
    providedIn: 'root'
})
export class CardDetailsResolver implements Resolve<Card> {
    constructor(private cardService: CardService) { }

    resolve(route: import("@angular/router").ActivatedRouteSnapshot,
        state: import("@angular/router").RouterStateSnapshot): import("rxjs").Observable<Card> | Promise<Card> {

        const id = +route.paramMap.get('id');
        this.cardService.onSelectedCardId(id);

        // return this.cardService.getCardById(id);
        return this.cardService.card$;
    }
}