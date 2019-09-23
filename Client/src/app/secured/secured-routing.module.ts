import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { DashboardComponent } from "./dashboard/dashboard.component";
import { AuthguardService } from "../core/authguard.service";

const routes: Routes = [
    { path: 'dashboard', component: DashboardComponent, canActivate: [AuthguardService] }
];

@NgModule({
    imports: [
        RouterModule.forChild(routes)
    ],
    exports: [
        RouterModule
    ],
    providers: [
        AuthguardService
    ]
})
export class SecuredRoutingModule { }