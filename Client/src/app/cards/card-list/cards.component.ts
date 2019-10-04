import { Component, Input, ChangeDetectionStrategy, ViewChild } from '@angular/core';
import { NgbModal, NgbActiveModal, NgbTypeaheadConfig, NgbTypeahead } from '@ng-bootstrap/ng-bootstrap';

import { CardService } from '../card.service';
import { Card, SearchTermCardFilter } from '../card';
import { map, catchError, debounceTime, switchMap, tap, distinctUntilChanged, merge, filter } from 'rxjs/operators';
import { combineLatest, BehaviorSubject, Observable, Subject, EMPTY } from 'rxjs';

@Component({
  templateUrl: './cards.component.html',
  styleUrls: ['./cards.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [NgbTypeaheadConfig]
})
export class CardsComponent {
  @ViewChild('instance', { static: false }) instance: NgbTypeahead;

  pageTitle = 'Cards';
  errorMessage: string;

  focus$ = new Subject<string>();
  click$ = new Subject<string>();

  _filter: SearchTermCardFilter = new SearchTermCardFilter();

  private selectedRaritySubject = new BehaviorSubject<string>('all');
  selectedRarityAction$ = this.selectedRaritySubject.asObservable();

  private errorMessageSubject = new Subject<string>();
  errorMessage$ = this.errorMessageSubject.asObservable();

  constructor(private _cardService: CardService,
    private _modalService: NgbModal,
    private _typeaheadConfig: NgbTypeaheadConfig) {

    _typeaheadConfig.showHint = false;
  }

  cards$ = this._cardService.searchResults$
    .pipe(
      catchError(err => this.handleError(err))
    )

  cardsTroop$ = this.cards$
    .pipe(
      map(cards =>
        cards.items.filter(card => card.type.toLocaleLowerCase() === 'troop')
      )
    )

  cardsSpell$ = this.cards$
    .pipe(
      map(cards =>
        cards.items.filter(card => card.type.toLocaleLowerCase() === 'spell')
      )
    )

  cardsBuilding$ = this.cards$
    .pipe(
      map(cards =>
        cards.items.filter(card => card.type.toLocaleLowerCase() === 'building')
      )
    )

  search = (text$: Observable<string>) =>
    text$.pipe(
      debounceTime(1000),
      distinctUntilChanged(),
      merge(this.focus$),
      merge(this.click$.pipe(filter(() => !this.instance.isPopupOpen()))),
      switchMap(() =>
        this.cards$.pipe(
          map(cards =>
            cards.items.slice(0, 10).map(card => card.name)
          ),
          catchError(err => this.handleError(err))
        )
      )
    )

  vm$ = combineLatest([
    this.cardsTroop$,
    this.cardsSpell$,
    this.cardsBuilding$
  ])
    .pipe(
      map(([cardsTroop, cardsSpell, cardsBuilding]) =>
        ({ cardsTroop, cardsSpell, cardsBuilding })
      )
    )

  onRarityChange(rarity: string) {
    this._filter.rarity = rarity;

    this.selectedRaritySubject.next(rarity);

    this._cardService.onSearch(this._filter);
  }

  onSelectedCardId(id: number) {
    this._cardService.onSelectedCardId(id);
  }

  onSearchChanged(keyword: string) {
    this._filter.searchTerm = keyword;
    this._filter.rarity = this.selectedRaritySubject.value;

    this._cardService.onSearch(this._filter);
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
      errorMessage = `Backend returned code ${err.status}: ${err.error}`;
    }

    console.error(`CardsComponent: ${errorMessage}`);

    return EMPTY;
    // return throwError(errorMessage);
  }
}

@Component({
  selector: 'app-card-modal-content',
  template: `<div class="modal-header">
              <h4 class="modal-title">{{selectedCard?.name}}</h4>
              <button type="button" class="close" aria-label="Close" (click)="activeModal.dismiss('Cross click')">
              <span aria-hidden="true">&times;</span>
              </button>
          </div>
          <div class="modal-body">
          <div class="card-detail-card">
          <div class="card-detail-card-container">
              <div class='row'>
                  <div class='col-sm-3'>
                      <img class="img-fluid"
                  [src]='selectedCard.imageUrl'
                  [title]='selectedCard.name' />
                  </div>
                  <div class='col-sm-9'>
              <div class='row'>
                <div class='col-sm-3'><strong>Name:</strong></div>
                <div class='col-sm-9'>{{selectedCard?.name}}</div>
              </div>
              <div class='row'>
                <div class='col-sm-3'><strong>Description:</strong></div>
                <div class='col-sm-9'>{{selectedCard?.description}}</div>
              </div>
              <div class='row'>
                <div class='col-sm-3'><strong>Rarity:</strong></div>
                <div class='col-sm-9'>{{selectedCard?.rarity}}</div>
              </div>
              <div class='row'>
                <div class='col-sm-3'><strong>Type:</strong></div>
                <div class='col-sm-9'>{{selectedCard?.type}}</div>
              </div>
              <div class='row'>
                <div class='col-sm-3'><strong>Elixir Cost:</strong></div>
                <div class='col-sm-9'>{{selectedCard?.elixirCost}}</div>
              </div>
                  </div>
              </div>
          </div>
      </div>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-outline-dark" (click)="activeModal.close('Close click')">Close</button>
        </div>`,
  styles: [`$cardTitleColor: #007bff;
        $titleBackgroundColor: whitesmoke;
        /* On mouse-over, add a deeper shadow */
        /* Add some padding inside the card container */
        .card-detail-card {
          box-shadow: 0 4px 8px 0 rgba(0,0,0,0.2);
          transition: 0.3s;
          border-radius: 5px;
          padding-bottom: 20px;
          margin-top: 20px;
          .card-detail-card-title {
            font-size: 1.2rem;
            font-weight: bold;
            padding: 5px;
            background-color: $titleBackgroundColor;
            color: $cardTitleColor;
          }
          &:hover {
            box-shadow: 0 8px 16px 0 rgba(0,0,0,0.2);
          }
        }
        .card-detail-card-container {
          padding: 20px;
          margin: 10px;
        }
        img {
          border-radius: 5px 5px 0 0;
        }`]
})
export class CardModalContentComponent {
  @Input() selectedCard: Card;

  constructor(public activeModal: NgbActiveModal) { }
}
