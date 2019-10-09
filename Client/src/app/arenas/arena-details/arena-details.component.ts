import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ArenaService } from '../arena.service';
import { map, mergeMap, catchError } from 'rxjs/operators';
import { combineLatest, EMPTY } from 'rxjs';

@Component({
  templateUrl: './arena-details.component.html',
  styleUrls: ['./arena-details.component.scss']
})
export class ArenaDetailsComponent {
  pageTitle = 'Arena Details';

  constructor(private _route: ActivatedRoute,
    private _router: Router,
    private _arenaService: ArenaService) { }

  arena$ = this._route.params
    .pipe(
      map(params => params['id']),
      mergeMap(this._arenaService.arena$),
      catchError(err => this.handleError(err))
    )

  arenaCardsUnlock$ = this._route.params
    .pipe(
      map(params => params['id']),
      mergeMap(this._arenaService.arenaCardsUnlock$),
      catchError(err => this.handleError(err))
    )

  arenaChestsUnlock$ = this._route.params
    .pipe(
      map(params => params['id']),
      mergeMap(this._arenaService.arenaChestsUnlock$),
      catchError(err => this.handleError(err))
    )

  vm$ = combineLatest([this.arena$, this.arenaCardsUnlock$, this.arenaChestsUnlock$])
    .pipe(
      map(([arena, arenaCardsUnlock, arenaChestsUnlock]) =>
        ({ arena, arenaCardsUnlock, arenaChestsUnlock })
      ),
      catchError(err => this.handleError(err))
    )

  private handleError(err: any) {
    // in a real world app, we may send the server to some remote logging infrastructure
    // instead of just logging it to the console
    let errorMessage: string;
    if (err.error instanceof ErrorEvent) {
      // A client-side or network error occurred. Handle it accordingly.
      errorMessage = `An error occurred: ${err.error.message}`;
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong,
      errorMessage = `Backend returned code ${err.status}: ${err.error}`;
    }

    console.error(`CardsComponent: ${errorMessage}`);

    return EMPTY;
    // return throwError(errorMessage);
  }

  onBack(): void {
    this._router.navigate(['/arenas']);
  }
}