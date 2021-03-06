import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { catchError } from 'rxjs/operators';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private router: Router, private toastr: ToastrService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError(error => {
        if (error){
          switch (error.status) {
            case 400:
              error.statusText = "Bad Request";
              if (error.error.errors){
                const modaalStateErrors = [];
                for (const key in error.error.errors) {
                  if (error.error.errors[key]) {
                    modaalStateErrors.push(error.error.errors[key])
                  }
                }
                throw modaalStateErrors.flat();
              } else {
                this.toastr.error(error.statusText, error.status);
              }
              break;

              case 401:
                error.statusText = "Unauthorized";
                this.toastr.error(error.statusText, error.status);
                break;

              case 404:
                this.router.navigateByUrl('/not-found');
                break;

              case 500:
                const navigationExtras: NavigationExtras = {state: {error: error.error}};
                this.router.navigateByUrl('server-error', navigationExtras);
                break;
            default:
              this.toastr.error('Something Unexpected went horribly wrong!')
              console.log(error);
              break;
          }
        }
        return throwError(error);
      })
    )
  }
}
