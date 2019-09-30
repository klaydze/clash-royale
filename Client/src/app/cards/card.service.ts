import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError, combineLatest, BehaviorSubject, EMPTY, Subject } from 'rxjs';
import { map, catchError, publishReplay, refCount, tap, switchMap, shareReplay, debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { ApiHelper } from '../shared/Helper';
import { Card, SearchTermCardFilter } from './card';
import { PagedResults } from '../models/PagedResults';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  'providedIn': 'root'
})
export class CardService {
  private _cardsApi = `${ApiHelper.clashRoyaleApi}/cards`;
  private _cardImageApi = `${ApiHelper.clashRoyaleImageApi}/cards`;

  private errorMessageSubject = new Subject<string>();
  errorMessage$ = this.errorMessageSubject.asObservable();

  private selectedCardIdSubject = new BehaviorSubject<number | null>(null);
  selectedCardIdAction$ = this.selectedCardIdSubject.asObservable();

  private searchFilterSubject = new BehaviorSubject<SearchTermCardFilter>(new SearchTermCardFilter());
  searchFilterAction$ = this.searchFilterSubject.asObservable();

  constructor(private _http: HttpClient,
    private _spinner: NgxSpinnerService) { }

  /**
   * Get all cards.
   * @returns Observable<Card[]>
 */
  cards$ = this._http.get<PagedResults<Card>>(this._cardsApi)
    .pipe(
      map(cards =>
        cards.items.map(card => ({
          ...card,
          imageUrl: `${this._cardImageApi}/${card.idName}.png`
        }) as Card)
      ),
      shareReplay(1),
      catchError(this.handleError)
    );

  /**
   * Retrieve single card based on card id.
   * @returns Observable<Card>
   */
  card$ = this.selectedCardIdAction$
    .pipe(
      switchMap(id =>
        this._http.get<Card>(`${this._cardsApi}/${id}`)
          .pipe(
            map(card => ({
              ...card,
              imageUrl: `${this._cardImageApi}/${card.idName}.png`
            }) as Card)
          )
      ),
      catchError(this.handleError)
    );


  onSelectedCardId(id: number) {
    this.selectedCardIdSubject.next(id);
  }

  /**
   * Retrieve card based on card id name
   * @param idName Card id name to search
   */
  getCardByIdName(idName: string): Observable<Card> {
    return this._http.get<Card>(`${this._cardsApi}/${idName}`)
      .pipe(
        map(card => ({
          ...card,
          imageUrl: `${this._cardImageApi}/${card.idName}.png`
        }) as Card),
        catchError(this.handleError)
      );
  }

  /**
   * Search cards base on rarity and card name.
   */
  searchResults$ = this.searchFilterAction$
  .pipe(
    tap(() => this._spinner.show()),
    map(filter => {
      if ((filter.rarity.toLocaleLowerCase() === 'all') &&
        (filter.searchTerm === undefined || filter.searchTerm.length === 0)) {
        return `${this._cardsApi}`;
      }

      if ((filter.rarity.toLocaleLowerCase() !== 'all') &&
        (filter.searchTerm === undefined || filter.searchTerm.length === 0)) {
        return `${this._cardsApi}?search=rarity eq ${filter.rarity}`;
      }

      if (filter.rarity.toLocaleLowerCase() === 'all' &&
        filter.searchTerm.length > 0) {
        return `${this._cardsApi}?search=name co ${filter.searchTerm}`;
      }

      if (filter.rarity.toLocaleLowerCase() !== 'all' &&
        filter.searchTerm.length > 0) {
        return `${this._cardsApi}?search=rarity eq ${filter.rarity}&search=name co ${filter.searchTerm}`;
      }
    }),
    debounceTime(300),
    distinctUntilChanged(),
    switchMap(filter => this._http.get<PagedResults<Card>>(filter)
      .pipe(
        map(cards =>
          cards.items.map(card => ({
            ...card,
            imageUrl: `${this._cardImageApi}/${card.idName}.png`
          }) as Card)
        )
      )
    ),
    shareReplay(1),
    tap(() => this._spinner.hide()),
    catchError(err => {
      this.errorMessageSubject.next(err);
      return EMPTY;
    })
  );

  onSearch(filter: SearchTermCardFilter) {
    this.searchFilterSubject.next(filter);
  }

  /**
   * Save a new card information in the database.
   * @param newCardInfo New card information to save.
   */
  saveCardInformation(newCardInfo: Card): Observable<Card> {
    let headers = new HttpHeaders({
      'Content-Type': 'application/json; charset=utf-8'
    });

    return this._http.post<Card>(`${this._cardsApi}`, JSON.stringify(newCardInfo), { headers: headers });
  }

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
      errorMessage = `Backend returned code ${err.status}: ${err.error.message}`;
    }

    console.error(`CardService: ${errorMessage}`);

    return EMPTY;
    // return throwError(errorMessage);
  }
}
