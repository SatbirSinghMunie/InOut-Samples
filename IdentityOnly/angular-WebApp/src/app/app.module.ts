import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule} from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { OAuthModule, OAuthStorage } from 'angular-oauth2-oidc';
import { HomeComponent } from './home/home.component';

// We need a factory, since localStorage is not available during AOT build time.
export function storageFactory() : OAuthStorage {
  return localStorage;
}

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    OAuthModule.forRoot({resourceServer: {
      allowedUrls: ['http://paxcomtestclientapi.azurewebsites.net'],
      sendAccessToken: true
    }
    })
  ],
  providers: [
    { provide: OAuthStorage, useFactory: storageFactory }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
