import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot } from "@angular/router";
import { Observable } from "rxjs";

import { Arena } from "./arena";
import { ArenaService } from "./arena.service";
import { Injectable } from "@angular/core";

@Injectable({
    providedIn: 'root'
})
export class ArenaDetailsResolver implements Resolve<Arena> {

    constructor(private arenaService: ArenaService) { }

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Arena | Observable<Arena> | Promise<Arena> {
        const id = +route.paramMap.get('id');

        return this.arenaService.getArenaById(id);
    }

}