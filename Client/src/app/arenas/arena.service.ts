import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Arena } from './arena';
import { Observable } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { ApiHelper } from '../shared/Helper';
import { PagedResults } from '../shared/PagedResults';
import { Card } from '../cards/card';

@Injectable({
  providedIn: 'root'
})
export class ArenaService {
  private _arenaApi = `${ApiHelper.ClashRoyaleApi}/arenas`;
  private _arenaImageApi = `${ApiHelper.ClashRoyaleImageApi}/arenas`;

  constructor(private http: HttpClient) { }

  /**
   * Retrieve all arenas
   */
  getArenas(): Observable<Arena[]> {
    return this.http.get<PagedResults<Arena>>(this._arenaApi)
      .pipe(
        map(data => {
          const modifiedData: Arena[] = [];

          data.items.forEach(arena => {
            arena.imageUrl = `${this._arenaImageApi}/${arena.idName}.png`;
            modifiedData.push(arena);
          });

          return modifiedData;
        }),
        catchError(this.handleError)
      );
  }

  /**
   * Retrieve arena base on arena id
   * @param id Arena Id to search
   */
  getArenaById(id: number): Observable<Arena> {
    return this.http.get<Arena>(`${this._arenaApi}/${id}`)
      .pipe(
        map(data => {
          data.imageUrl = `${this._arenaImageApi}/${data.idName}.png`;
          return data;
        }),
        catchError(this.handleError)
      );
  }

  /**
   * Retrieve list of cards that can be unlocked in the said arena.
   * @param id Arena Id
   */
  getUnlockCardsByArenaId(id: number): Observable<Card[]> {
    return this.http.get<Card[]>(`${this._arenaApi}/${id}/cards`)
    .pipe(
      map(data => {
        const modifiedData: Card[] = [];

        data.forEach(card => {
          card.imageUrl = `${ApiHelper.ClashRoyaleImageApi}/cards/${card.idName}.png`;
          modifiedData.push(card);
        });

        return modifiedData;
      }),
      catchError(this.handleError)
    );
  }

  /**
   * Retrieve list of chests that can be unlocked in the said arena.
   * @param id Arena Id
   */
  getUnlockChestsByArenaId(id: number): Observable<Chest[]> {
    return this.http.get<Chest[]>(`${this._arenaApi}/${id}/chests`)
    .pipe(
      map(data => {
        const modifiedData: Chest[] = [];

        data.forEach(chest => {
          chest.imageUrl = `${ApiHelper.ClashRoyaleImageApi}/chests/${chest.idName}.png`;
          modifiedData.push(chest);
        });

        return modifiedData;
      }),
      catchError(this.handleError)
    );
  }

  private handleError(err: HttpErrorResponse) {
    console.log('ArenaService: ' + err.message);
    return Observable.throw(err.message);
  }
}
