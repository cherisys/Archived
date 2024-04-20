import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  apiUrl = "http://localhost:26012";
  constructor(private http:HttpClient) { }

  login(username: string, password: string){
    let postHeaders = new HttpHeaders();
    return this.http.post<any>(`${this.apiUrl}/users/authenticate`, { username, password })
            .pipe(map(user => {
                // login su{ccessful if there's a user in the response
                if (user) {
                    // store user details and basic auth credentials in local storage 
                    // to keep user logged in between page refreshes
                    user.authdata = window.btoa(username + ':' + password);
                    localStorage.setItem('currentUser', JSON.stringify(user));
                }

                return user;
            }));
  }

  logout() {
    localStorage.removeItem('currentUser');
  }
}
