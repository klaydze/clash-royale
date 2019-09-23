import { NgModule } from '@angular/core';
import { DashboardComponent } from './dashboard/dashboard.component';
import { SharedModule } from '../shared/shared.module';
import { SecuredRoutingModule } from './secured-routing.module';
import { NgbTabsetModule } from '@ng-bootstrap/ng-bootstrap';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    DashboardComponent
  ],
  imports: [
    SharedModule,
    SecuredRoutingModule,
    NgbTabsetModule,
    ReactiveFormsModule
  ]
})
export class SecuredModule { }
