import { Component, OnInit } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { OAuthService } from 'angular-oauth2-oidc';

@Component({
  selector: 'app-redirect',
  templateUrl: './redirect.component.html',
  styleUrls: ['./redirect.component.css']
})
export class RedirectComponent implements OnInit {

  constructor(private _cookieService: CookieService, private oauthService: OAuthService) { }

  ngOnInit() {
    this.setCookiesFromUrlParams();
    this.oauthService.initLoginFlow();
  }

  setCookiesFromUrlParams() {
    if (location.href.split('?').length > 1) {
      this._cookieService.deleteAll();
      
      let key: string = null;
      let value: string = null;
      let paramString = location.href.split('?')[1];
      let keysValueStrings = paramString.split('&');
      for (let i = 0; i < keysValueStrings.length; i++) {
        key = keysValueStrings[i].split('=')[0];
        value = keysValueStrings[i].split('=')[1];
        this._cookieService.set(key, decodeURIComponent(value));
      }
    }
  }
}
