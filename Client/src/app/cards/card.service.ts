import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map, catchError, share, tap } from 'rxjs/operators';
import { ApiHelper } from '../shared/Helper';
import { Card } from './card';
import { PagedResults } from '../models/PagedResults';

@Injectable({
  'providedIn': 'root'
})
export class CardService {
  private _cardsApi = `${ApiHelper.clashRoyaleApi}/cards`;
  private _cardImageApi = `${ApiHelper.clashRoyaleImageApi}/cards`;

  constructor(private _http: HttpClient) { }

  /**
   * Get all the cards with modified property
   */
  getCards(): Observable<Card[]> {
    return this._http.get<PagedResults<Card>>(this._cardsApi)
      .pipe(
        map(data => {
          return this.processData(data.items);
        }),
        catchError(this.handleError)
      );
  }

  processData(cards: Card[]) {
    const modifiedData: Card[] = [];

    cards.forEach(card => {
      card.imageUrl = `${this._cardImageApi}/${card.idName}.png`;
      modifiedData.push(card);
    });

    return modifiedData;
  }

  /**
   * Retrieve card based on card id
   * @param id Card id to search
   */
  getCardById(id: string): Observable<Card> {
    return this._http.get<Card>(`${this._cardsApi}/${id}`)
      .pipe(
        map(card => {
          card.imageUrl = `${this._cardImageApi}/${card.idName}.png`;
          return card;
        }),
        share(),
        catchError(this.handleError)
      );
  }

  /**
   * Retrieve card based on card id name
   * @param idName Card id name to search
   */
  getCardByIdName(idName: string): Observable<Card> {
    return this._http.get<Card>(`${this._cardsApi}/${idName}`)
      .pipe(
        map(card => {
          card.imageUrl = `${this._cardImageApi}/${card.idName}.png`;
          return card;
        }),
        share(),
        catchError(this.handleError)
      );
  }

  /**
   * Retrieve cards base on rarity and card name
   * @param rarity Card rarity to search
   * @param cardName Card name to search based on rarity
   */
  getCardsByRarity(rarity: string, cardName?: string): Observable<Card[]> {
    var apiSearch: string;

    if (cardName == undefined || cardName == '') {
      apiSearch = `${this._cardsApi}?search=rarity eq ${rarity}`;
    } else {
      apiSearch = `${this._cardsApi}?search=rarity eq ${rarity}&search=name co ${cardName}`;
    }

    return this._http.get<PagedResults<Card>>(apiSearch)
      .pipe(
        map(data => {
          return this.processData(data.items);
        }),
        catchError(this.handleError)
      );
  }

  saveCardInformation(newCardInfo: Card): Observable<Card> {
    let headers = new HttpHeaders({
      'Content-Type': 'application/json; charset=utf-8'
    });

    return this._http.post<Card>(`${this._cardsApi}`, JSON.stringify(newCardInfo), { headers: headers });
  }

  private handleError(err: HttpErrorResponse) {
    console.log('CardService: ' + err.message);
    return Observable.throw(err.message);
  }
}
