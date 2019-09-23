import { Component, OnInit } from "@angular/core";
import { AuthService } from "./core/auth.service";
import { Router, ActivatedRoute } from "@angular/router";

@Component({
    templateUrl: './auth-callback.component.html',
    styles: []
})
export class AuthCallbackComponent implements OnInit {

    error: boolean;

    constructor(private _authService: AuthService,
        private router: Router,
        private route: ActivatedRoute) { }

    async ngOnInit() {
        // check for error
        //if (this.route.snapshot.fragment.indexOf('error') >= 0) {
        if (this.route.snapshot.queryParams['error'] == 'access_denied') {
            this.error = true;
            return;
        }

        await this._authService.completeAuthentication();
        this.router.navigate(['/home']);
    }

}