import { Component, OnInit } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor(private oauthService: OAuthService,private httpClient: HttpClient) { }

  ngOnInit() {
  }
  public login() {
      this.oauthService.initLoginFlow();
  }

  public logoff() {
      this.oauthService.logOut();
  }

  public get name() {
      let claims = this.oauthService.getIdentityClaims();
      if (!claims) return null;

      let token = this.parseJwt(this.oauthService.getAccessToken());
      console.log(token);
      return token.name;
  }

  public userInfo:[];

  private apiBaseUrl : string = "http://localhost:5200/";

  getPublicData(){
    this.httpClient.get<any>(this.apiBaseUrl + "api/values").subscribe(res => {
      alert(JSON.stringify(res));
    });
  }
  
  getPrivateData(){
    this.httpClient.get<any>(this.apiBaseUrl + "api/values/2").subscribe(res => {
      alert(JSON.stringify(res));
    },
    error => {
      alert(error.statusText);
    });
  }

  //Get Claims on client side
  parseJwt (token) {
    var base64Url = token.split('.')[1];
    var base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    var jsonPayload = decodeURIComponent(atob(base64).split('').map(function(c) {
        return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
    }).join(''));

    return JSON.parse(jsonPayload);
  };

  //Get claims from server side
  getUserInfo(){
    this.httpClient.get<any>(this.apiBaseUrl + "api/users/claims").subscribe(res => {
      this.userInfo = res;
    });
  }
}
