import { Component, OnInit, Input } from '@angular/core';
import { Card, CardStatistics } from '../card';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-card-details',
  templateUrl: './card-details.component.html',
  styleUrls: ['./card-details.component.scss']
})
export class CardDetailsComponent implements OnInit {
  
  selectedCard: Card;
  colStatsPerLevel: string[];
  
  constructor(private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.selectedCard = data['resolvedSelectedCardData'];

      if (this.selectedCard) {
        if (this.selectedCard.cardStatistics.length > 0) {
          this.colStatsPerLevel = Object.getOwnPropertyNames(this.selectedCard.cardStatistics[0]);
        }
      }

    });
  }
}
