import { NgModule } from '@angular/core';
import { RegisterComponent } from './register/register.component';
import { AccountRoutingModule } from './account-routing.module';
import { AuthService } from '../core/auth.service';
import { SharedModule } from '../shared/shared.module';

@NgModule({
  declarations: [
    RegisterComponent
  ],
  imports: [
    SharedModule,
    AccountRoutingModule
  ],
  providers: [
    AuthService
  ]
})
export class AccountModule { }
