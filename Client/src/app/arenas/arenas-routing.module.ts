import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ArenasComponent } from './arena-list/arenas.component';
import { ArenaDetailsComponent } from './arena-details/arena-details.component';
import { ArenaResolver } from './arena-list/arena-resolver-service';
import { ArenaDetailsResolver } from './arena-details/arena-details-resolver.service';

const routes: Routes = [
  { path: '', component: ArenasComponent, resolve: { resolvedArenasData: ArenaResolver } },
  { path: ':id', component: ArenaDetailsComponent, resolve: { resolvedArenaData: ArenaDetailsResolver } }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  declarations: [],
  exports: [RouterModule]
})
export class ArenasRoutingModule { }
