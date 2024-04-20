import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  constructor(private http:HttpClient) { }

  login(username: string, password: string) {
      return this.http.post<any>(`${environment.API_URL}/users/authenticate`, { username: username, password: password })
          .pipe(map(user => {
              if (user && user.token) {
                  localStorage.setItem('currentUser', JSON.stringify(user));
              }

              return user;
          }));
  }

  logout() {
      // remove user from local storage to log user out
      localStorage.removeItem('currentUser');
  }
}
