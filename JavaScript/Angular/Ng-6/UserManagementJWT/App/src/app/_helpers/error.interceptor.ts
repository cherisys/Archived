import {Injectable} from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { HttpRequest, HttpHandler, HttpInterceptor, HttpEvent } from '@angular/common/http';
import {catchError} from 'rxjs/operators';
import { AuthenticationService } from '../_services';


@Injectable()

export class ErrorInterceptor implements HttpInterceptor {

    constructor(private authService: AuthenticationService) {}
    intercept(request:HttpRequest<any>,next:HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(request).pipe(catchError(err=> {
            if(err.status === 401){
                this.authService.logout();
                location.reload(true);
            }

            const error = err.error.message || err.statusText;
            return throwError(error);
        }))
    }
}