import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgbModal, NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

import { CardService } from '../card.service';
import { Card, CardDetail } from '../card';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  templateUrl: './cards.component.html',
  styleUrls: ['./cards.component.scss']
})
export class CardsComponent implements OnInit {
  pageTitle = 'Cards';
  errorMessage: string;

  // For filter
  filteredCardsDetail: CardDetail = new CardDetail();
  cards: Card[] = [];
  filteredCards: Card[];
  selectedCard: Card;
  selectedRarity = 'All';

  // For pagination
  page = 1;
  totalRecordCount: number;

  _filterKeyword: string;
  get filterKeyword(): string {
    return this._filterKeyword;
  }
  set filterKeyword(value: string) {
    this._filterKeyword = value;
    this.filteredCards = this.performFilter(this.filterKeyword);
  }

  constructor(private _cardService: CardService,
    private _modalService: NgbModal,
    private _route: ActivatedRoute,
    private _spinner: NgxSpinnerService)  { }

  performFilter(keyword: string): Card[] {
    if ((this.selectedRarity) && (this.selectedRarity.toLocaleLowerCase() === 'all')) {
      this.filteredCards = this.cards.filter((card: Card) =>
        (card.name.toLocaleLowerCase().indexOf(keyword.toLocaleLowerCase()) !== -1 ||
          card.type.toLocaleLowerCase().indexOf(keyword.toLocaleLowerCase()) !== -1));
    } else if (this.filterKeyword == undefined || this.filterKeyword == '') {
      this.getCardsByRarity(this.selectedRarity);
    } else {
      this.filteredCards = this.cards.filter((card: Card) =>
        (card.name.toLocaleLowerCase().indexOf(keyword.toLocaleLowerCase()) !== -1 ||
          card.type.toLocaleLowerCase().indexOf(keyword.toLocaleLowerCase()) !== -1) &&
        (card.rarity.toLocaleLowerCase() === this.selectedRarity.toLocaleLowerCase()));
    }

    this.groupTheCards(this.filteredCards);
    this.totalRecordCount = this.filteredCards.length;

    return this.filteredCards;
  }

  /**
   * Local method to retrieve all the list of cards
   */
  getAllCards(): void {
    this._spinner.show();
    
    this._cardService.getCards()
      .subscribe(
        data => {
          this.cards = data;
          this.filteredCards = this.cards;

          this.groupTheCards(data);

          this._spinner.hide();
        }
      );
  }

  /**
   * Local method to retrieve all the list of cards by rarity
   * @param rarity Find cards by rarity
   */
  getCardsByRarity(rarity: string): void {
    this._spinner.show();

    this._cardService.getCardsByRarity(rarity, this.filterKeyword)
      .subscribe(
        data => {
          this.cards = data;
          this.filteredCards = data;
          this.totalRecordCount = this.filteredCards.length;

          this.groupTheCards(data);

          this._spinner.hide();

          return this.filteredCards;
        }
      );
  }

  groupTheCards(data: Card[]): void {
    this.filteredCardsDetail = new CardDetail();

    data.forEach(c => {
      if (c.type.toLocaleLowerCase() === "troop") {
        this.filteredCardsDetail.troops.push(c);
      } else if (c.type.toLocaleLowerCase() === "spell") {
        this.filteredCardsDetail.spells.push(c);
      } else {
        this.filteredCardsDetail.buildings.push(c);
      }
    });
  }

  /**
   * Open a modal to show info of card
   * @param id Card id to show
   */
  open(id: number): void {
    this.selectedCard = this.filteredCards.find(card => card.id === id);

    if (this.selectedCard) {
      const modalRef = this._modalService.open(CardModalContentComponent, { size: 'lg' });
      modalRef.componentInstance.selectedCard = this.selectedCard;
    }
  }

  /**
   * Fires every time rarity drop down changed
   * @param rarity Selected rarity in the drop down
   */
  onRarityChange(rarity: string) {
    this.selectedRarity = rarity;
    // this._filterKeyword = '';

    if (this.selectedRarity.toLocaleLowerCase() === 'all') {
      this.getAllCards();
    } else {
      this.getCardsByRarity(this.selectedRarity);
    }
  }

  ngOnInit() {
    this._spinner.show();

    this._route.data
      .subscribe(data => {
        this.cards = data['resolvedCardsData'];
        this.filteredCards = this.cards;

        this.groupTheCards(this.cards);

        this._spinner.hide();
      });
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
