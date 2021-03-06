import { Injectable } from "@angular/core";
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot } from "@angular/router";
import { Observable } from "rxjs";

import { Arena } from "../arena";
import { ArenaService } from "../arena.service";
import { map } from "rxjs/operators";

@Injectable({
    providedIn: 'root'
})
export class ArenaResolver implements Resolve<Arena[]> {

    constructor(private arenaService: ArenaService) { }

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Arena[] | Observable<Arena[]> | Promise<Arena[]> {
        // return this.arenaService.getArenas();
        return this.arenaService.arenas$
            .pipe(
                map(arenas =>
                    arenas.items
                )
            )
    }
}