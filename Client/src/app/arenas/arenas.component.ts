import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Arena } from './arena';
import { ArenaService } from './arena.service';

@Component({
  templateUrl: './arenas.component.html',
  styleUrls: ['./arenas.component.scss']
})
export class ArenasComponent implements OnInit {
  pageTitle = 'Arenas';
  errorMessage: string;

  // For filter
  arenas: Arena[] = [];
  filteredArenas: Arena[];

  constructor(private _route: ActivatedRoute,
            private arenaService: ArenaService) { }

  ngOnInit() {
    this._route.data.subscribe(data => {
      this.arenas = data['resolvedArenasData'];
      this.filteredArenas = this.arenas;
    });
  }
}
