import { Component, OnInit } from '@angular/core';
import { OAuthService, JwksValidationHandler, AuthConfig } from 'angular-oauth2-oidc';
import { Router } from '@angular/router';
import { inoutConstants } from './inout-constants';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  title = 'angular-WebApp';
  isFirstToken: boolean = true;

  authConfig: AuthConfig = {
    issuer: inoutConstants.identityUrl,
    redirectUri: `${location.origin}`,
    clientId: 'js_pkce',						//Signifies this is Authorization Code flow with PKCE
    responseType: 'code',						//Signifies this is Authorization Code flow with PKCE
    scope: `openid profile offline_access ${inoutConstants.applicationScope}`,
    postLogoutRedirectUri: `${location.origin}`,
    sessionChecksEnabled: true,					//Set to true of want to implement single signout
    sessionCheckIntervall: 5000,				//Milliseconds after which application checks whether user has been logged out or not 
    requireHttps: false,						//Keep this true for production
    // showDebugInformation: false,					//Keep this false for production
  }
  
  constructor(private oauthService: OAuthService,private _router: Router) {
    this.oauthService.configure(this.authConfig);
    this.oauthService.tokenValidationHandler = new JwksValidationHandler();

    //If you don't want to display a login form that tells the user that they are redirected to the identity server, 
    //you can use the convenience function this.oauthService.loadDiscoveryDocumentAndLogin(); 
    //instead of this.oauthService.loadDiscoveryDocumentAndTryLogin()
    this.oauthService.loadDiscoveryDocumentAndTryLogin();
    this.oauthService.setupAutomaticSilentRefresh();
  }

  ngOnInit(): void {
    this.customizeAuthentication();
  }

  //Bind token received event. User is logged in after this event.
  //Keep checking user status with identity server once user is logged in.
  //Perform follow up steps only after successful log in.
  customizeAuthentication(){
    this.oauthService.events.subscribe(event => {
      if (this.isFirstToken && event.type == 'token_received') {
        this.isFirstToken = false;
        this.oauthService.restartSessionChecksIfStillLoggedIn();
      } 
    });
    this.storeSessionState();
  }

  storeSessionState(){
    let keyName = "session_state";
    let queryString = location.search.substring(1);
    let parameters = queryString.split('&');
    let parameterKeys = [];
    let parameterValues = [];
    for(let i =0; i< parameters.length; i++){
      parameterKeys.push(parameters[i].split('=')[0]);
      parameterValues.push(parameters[i].split('=')[1]);
    }
    if(parameterKeys.indexOf(keyName) > -1){
      let sessionStateValue = parameterValues[parameterKeys.indexOf("session_state")];
      localStorage.setItem(keyName,sessionStateValue);
    }
  }
}
