import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map, catchError, share } from 'rxjs/operators';
import { ApiHelper } from '../shared/Helper';
import { Card } from './card';
import { PagedResults } from '../shared/PagedResults';

@Injectable({
  'providedIn': 'root'
})
export class CardService {
  private _cardsApi = `${ApiHelper.ClashRoyaleApi}/cards`;
  private _cardImageApi = `${ApiHelper.ClashRoyaleImageApi}/cards`;

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

    // return this.getCards()
    //   .pipe(
    //     map(data => {
    //       if (cardName) {
    //         return data.filter((card: Card) => card.name.toLocaleLowerCase().indexOf(cardName) !== -1 &&
    //           card.rarity.toLocaleLowerCase() === rarity.toLocaleLowerCase());
    //       } else {
    //         return data.filter((card: Card) =>
    //           card.rarity.toLocaleLowerCase() === rarity.toLocaleLowerCase());
    //       }
    //     }),
    //     share(),
    //     catchError(this.handleError)
    //   );
  }

  private handleError(err: HttpErrorResponse) {
    console.log('CardService: ' + err.message);
    return Observable.throw(err.message);
  }
}
