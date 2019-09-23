import { Injectable } from '@angular/core';
import { UserManager, User, WebStorageStateStore, Log } from 'oidc-client';
import { ApiHelper } from '../shared/Helper';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { AuthContext } from '../models/auth-context';
import { catchError } from 'rxjs/operators';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private _userManager: UserManager;
  private _user: User;
  authContext: AuthContext;

  constructor(private httpClient: HttpClient) {
    var config = {
      authority: ApiHelper.stsAuthority,
      client_id: ApiHelper.clientId,
      redirect_uri: `${ApiHelper.clientRoot}assets/oidc-login-redirect.html`,
      // redirect_uri: `${ApiHelper.clientRoot}auth-callback`,
      scope: 'openid profile email custom.profile cr_api.read_only cr_api.read_write cr_api.admin',
      // response_type: 'id_token token',
      response_type: 'code',
      post_logout_redirect_uri: `${ApiHelper.clientRoot}?postLogout=true`,
      userStore: new WebStorageStateStore({ store: window.localStorage })
      // automaticSilentRenew: true,
      // silent_redirect_uri: `${ApiHelper.clientRoot}assets/silent-redirect.html`
    };

    this._userManager = new UserManager(config);

    this._userManager.getUser().then(user => {
      if (user && !user.expired) {
        this._user = user;
        this.loadSecurityContext();
      }
    });

    this._userManager.events.addUserLoaded(args => {
      this._userManager.getUser().then(user => {
        this._user = user;
        this.loadSecurityContext();
      });
    });
  }

  // Use this when you handle callback using component by calling this method OnInit
  async completeAuthentication(): Promise<void> {
    const user = await this._userManager.signinRedirectCallback();
    window.history.replaceState({}, window.document.title, window.location.origin);
    window.location.href = "/";
    this._user = user;
  }

  login(): Promise<any> {
    return this._userManager.signinRedirect();
  }

  logout(): Promise<any> {
    return this._userManager.signoutRedirect();
  }

  isLoggedIn(): boolean {
    return this._user &&
      this._user.access_token &&
      !this._user.expired;
  }

  getAccessToken(): string {
    return this._user ? this._user.access_token : '';
  }

  signoutRedirectCallback(): Promise<any> {
    return this._userManager.signoutRedirectCallback();
  }

  loadSecurityContext() {
    // TODO: Need to implement this in api project
    // this.httpClient.get<AuthContext>(`${ApiHelper.clientRoot}Account/AuthContext`).subscribe(context => {
    //   this.authContext = context;
    // }, error => console.error(Utils.formatError(error)));
  }

  register(userRegistration: any) {
    return this.httpClient.post(`${ApiHelper.stsAuthority}api/account`, userRegistration)
      .pipe(catchError(this.handleError));
  }

  private handleError(err: HttpErrorResponse) {
    console.log('AuthService: ' + err.message);
    return Observable.throw(err.message);
  }
}
