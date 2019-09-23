import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpEvent, HttpHandler, HttpRequest, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from './auth.service';
import { Router } from '@angular/router';
import { ApiHelper } from '../shared/Helper';
import { catchError } from 'rxjs/operators';

@Injectable({ providedIn: 'root' })
export class AuthInterceptor implements HttpInterceptor {

    constructor(private _authService: AuthService,
        private _router: Router) { }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        if (req.url.startsWith(ApiHelper.clashRoyaleApi)) {
            var accessToken = this._authService.getAccessToken();
            const headers = req.headers.set('Authorization', `Bearer ${accessToken}`);
            const authReq = req.clone({ headers });

            return next.handle(authReq).pipe(
                catchError(err => {
                    var respError = err as HttpErrorResponse;

                    if (respError && (respError.status === 401 || respError.status === 403)) {
                        this._router.navigate(['/unauthorized']);
                    }

                    return Observable.throw(err.statusText);
                })
            )
        } else {
            return next.handle(req);
        }
    }
}