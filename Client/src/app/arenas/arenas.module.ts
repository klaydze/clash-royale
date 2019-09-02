import { NgModule } from '@angular/core';
import { ArenasRoutingModule } from './arenas-routing.module';
import { SharedModule } from '../shared/shared.module';
import { ArenasComponent } from './arenas.component';
import { ArenaService } from './arena.service';
import { ArenaDetailsComponent } from './arena-details.component';

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
