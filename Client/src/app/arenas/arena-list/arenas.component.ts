import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { ArenaService } from '../arena.service';
import { catchError } from 'rxjs/operators';
import { EMPTY, } from 'rxjs';

@Component({
  templateUrl: './arenas.component.html',
  styleUrls: ['./arenas.component.scss']
})
export class ArenasComponent {
  pageTitle = 'Arenas';

  constructor(private _route: ActivatedRoute,
    private _arenaService: ArenaService) { }

  arenas$ = this._arenaService.arenas$
    .pipe(
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
}