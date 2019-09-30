import { Component, OnInit, ChangeDetectionStrategy, AfterViewInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CardService } from '../card.service';

@Component({
  selector: 'app-card-details',
  templateUrl: './card-details.component.html',
  styleUrls: ['./card-details.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class CardDetailsComponent implements OnInit {
  selectedCardId: number;
  colStatsPerLevel: string[];

  constructor(private route: ActivatedRoute,
    private cardService: CardService) { }

  selectedCard$ = this.cardService.card$;

  ngOnInit() {
    this.selectedCardId = this.route.snapshot.params['id'];

    this.cardService.onSelectedCardId(this.selectedCardId);
  }

  // ngAfterViewInit(): void {
  //   this.cardService.onSelectedCardId(this.selectedCardId);
  // }
}
