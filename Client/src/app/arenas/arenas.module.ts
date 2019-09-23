import { NgModule } from '@angular/core';
import { ArenasRoutingModule } from './arenas-routing.module';
import { SharedModule } from '../shared/shared.module';
import { ArenasComponent } from './arena-list/arenas.component';
import { ArenaService } from './arena.service';
import { ArenaDetailsComponent } from './arena-details/arena-details.component';

@NgModule({
  imports: [
    SharedModule,
    ArenasRoutingModule
  ],
  declarations: [
    ArenasComponent,
    ArenaDetailsComponent
  ],
  providers: [ArenaService]
})
export class ArenasModule { }
