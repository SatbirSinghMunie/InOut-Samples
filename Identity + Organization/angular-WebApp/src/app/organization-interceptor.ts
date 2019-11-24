
import { Injectable } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CookieService } from 'ngx-cookie-service';
import { inoutConstants } from './inout-constants';

@Injectable()
export class OrganizationInterceptor implements HttpInterceptor {

  constructor(private _cookieService: CookieService) {  }

  intercept (req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    let orgDataCookie = this._cookieService.get(inoutConstants.organizationCookiName);
    if (orgDataCookie) {
      let organizationId = 0;
      let newHeader: HttpHeaders;
      if(orgDataCookie){
        organizationId = JSON.parse(decodeURIComponent(orgDataCookie)).id; 
      }

      newHeader = req.headers.set(inoutConstants.organizationHeaderName, organizationId.toString()); 
      const authReq = req.clone({
        headers: newHeader 
      });

      return next.handle(authReq);
    } else {
      return next.handle(req);
    }
  }
}
