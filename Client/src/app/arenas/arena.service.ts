import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Arena } from './arena';
import { Observable, BehaviorSubject } from 'rxjs';
import { map, catchError, shareReplay, tap } from 'rxjs/operators';
import { ApiHelper } from '../shared/Helper';
import { PagedResults } from '../models/PagedResults';
import { Card } from '../cards/card';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root'
})
export class ArenaService {
  private _arenaApi = `${ApiHelper.clashRoyaleApi}/arenas`;
  private _arenaImageApi = `${ApiHelper.clashRoyaleImageApi}/arenas`;

  selectedArena$ = new BehaviorSubject<Arena>(null);
  selectedArenaCardsUnlock$ = new BehaviorSubject<Card[]>(null);
  selectedArenaChestsUnlock$ = new BehaviorSubject<Chest[]>(null);

  constructor(private http: HttpClient,
    private _spinner: NgxSpinnerService) { }

  /**
   * Get all arenas.
   * @returns Observable<PageResults<Arena>>
  */
  arenas$ = this.http.get<PagedResults<Arena>>(this._arenaApi)
    .pipe(
      tap(() => this._spinner.show()),
      map(arenas => ({
        ...arenas,
        items: arenas.items.map(arena => ({
          ...arena,
          imageUrl: `${this._arenaImageApi}/${arena.idName}.png`
        }) as Arena)
      }) as PagedResults<Arena>),
      shareReplay(1),
      tap(() => this._spinner.hide()),
      catchError(err => this.handleError(err))
    )

  /**
   * Retrieve single arena based on arena id.
   */
  arena$ = (id: number) => {
    this.http.get<Arena>(`${this._arenaApi}/${id}`)
      .pipe(
        map(arena => ({
          ...arena,
          imageUrl: `${this._arenaImageApi}/${arena.idName}.png`
        }) as Arena)
      ).subscribe(arena => this.selectedArena$.next(arena));

    return this.selectedArena$;
  }

  /**
   * Retrieve all cards that can be unlocked in the selected arena.
   */
  arenaCardsUnlock$ = (id: number) => {
    this.http.get<Card[]>(`${this._arenaApi}/${id}/cards`)
      .pipe(
        map(cards =>
          cards.map(card => ({
            ...card,
            imageUrl: `${ApiHelper.clashRoyaleImageApi}/cards/${card.idName}.png`
          }) as Card)
        ),
        catchError(err => this.handleError(err))
      ).subscribe(cards => this.selectedArenaCardsUnlock$.next(cards))

    return this.selectedArenaCardsUnlock$;
  }

  /**
   * Retrieve all chests that can be unlocked in the selected arena.
   */
  arenaChestsUnlock$ = (id: number) => {
    this.http.get<Chest[]>(`${this._arenaApi}/${id}/chests`)
      .pipe(
        map(chests =>
          chests.map(chest => ({
            ...chest,
            imageUrl: `${ApiHelper.clashRoyaleImageApi}/chests/${chest.idName}.png`
          }) as Chest)
        ),
        catchError(err => this.handleError(err))
      ).subscribe(chests => this.selectedArenaChestsUnlock$.next(chests))

    return this.selectedArenaChestsUnlock$;
  };

  private handleError(err: HttpErrorResponse) {
    console.log('ArenaService: ' + err.message);
    return Observable.throw(err.message);
  }
}
