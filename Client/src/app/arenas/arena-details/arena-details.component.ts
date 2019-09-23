import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Arena } from '../arena';
import { ArenaService } from '../arena.service';
import { Card } from '../../cards/card';

@Component({
  templateUrl: './arena-details.component.html',
  styleUrls: ['./arena-details.component.scss']
})
export class ArenaDetailsComponent implements OnInit {
  pageTitle = 'Arena Details';
  errorMessage: string;
  selectedArena: Arena;
  selectedArenaCardUnlocks: Card[];
  selectedArenaChestUnlocks: Chest[];
  cards: Card[];

  constructor(private _route: ActivatedRoute,
    private _router: Router,
    private _arenaService: ArenaService) { }

  ngOnInit() {
    this._route.data.subscribe(data => {
      this.selectedArena = data['resolvedArenaData'];

      this.getArenaCardUnlocks();
      this.getArenaChestUnlockes();
    });

    // const param = this._route.snapshot.paramMap.get('id');

    // if (param) {
    //   const id = param;
    //   this.getArenaById(id);
    // }
  }

  /**
   * Local method to retrieve all the cards that can be unlocked in the selected arena
   */
  getArenaCardUnlocks(): void {
    this._arenaService.getUnlockCardsByArenaId(this.selectedArena.id).subscribe(data => {
      this.selectedArenaCardUnlocks = data;
    });
  }

  /**
   * Local method to retrieve all the chests that can be unlocked in the selected arena
   */
  getArenaChestUnlockes(): void {
    this._arenaService.getUnlockChestsByArenaId(this.selectedArena.id).subscribe(data => {
      this.selectedArenaChestUnlocks = data;
    });
  }

  onBack(): void {
    this._router.navigate(['/arenas']);
  }

}
