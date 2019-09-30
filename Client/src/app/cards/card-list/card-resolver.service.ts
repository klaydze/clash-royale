import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot } from "@angular/router";
import { Injectable } from "@angular/core";
import { Observable, of } from "rxjs";

import { CardService } from "../card.service";
import { Card } from "../card";

@Injectable({
    providedIn: 'root'
})
export class CardResolver implements Resolve<Card[]> {

    constructor(private cardService: CardService) { }

    resolve(route: ActivatedRouteSnapshot, 
        state: RouterStateSnapshot): Card[] | Observable<Card[]> | Promise<Card[]> {
        return this.cardService.cards$;
    }

}