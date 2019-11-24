import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { OAuthModule, OAuthStorage } from 'angular-oauth2-oidc';
import { HomeComponent } from './home/home.component';
import { OrganizationInterceptor } from './organization-interceptor';
import { RedirectComponent } from './redirect/redirect.component';
import { CookieService } from 'ngx-cookie-service';
import { inoutConstants } from './inout-constants';

// We need a factory, since localStorage is not available during AOT build time.
export function storageFactory() : OAuthStorage {
  return localStorage;
}

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    RedirectComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    OAuthModule.forRoot({resourceServer: {
      allowedUrls: [inoutConstants.apiBaseUrl],
      sendAccessToken: true
    }
    })
  ],
  providers: [
    CookieService,
    { provide: HTTP_INTERCEPTORS, useClass: OrganizationInterceptor, multi: true },
    { provide: OAuthStorage, useFactory: storageFactory }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
