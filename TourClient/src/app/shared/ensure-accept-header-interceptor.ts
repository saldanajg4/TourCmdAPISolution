import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs';

export class EnsureAcceptHeaderInterceptor implements HttpInterceptor {
    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        if(!req.headers.has('Accept')){
            req = req.clone({headers: req.headers.set('Accept', 'application/json')});
        }
        return next.handle(req);
    }
}
