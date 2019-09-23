import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { tap } from 'rxjs/operators';

import { Arena } from '../arena';
import { NgxSpinnerService } from 'ngx-spinner';
import { ArenaService } from '../arena.service';

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
    private _spinner: NgxSpinnerService) { }

  ngOnInit() {
    this._spinner.show();

    this._route.data.subscribe(data => {
      this.arenas = data['resolvedArenasData'];
      this.filteredArenas = this.arenas;

      this._spinner.hide();
    });
  }
}
