import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from '../_models/User';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  getAll(){
    return this.http.get<User[]>(`${environment.API_URL}/users`);
  }

  getById(id: number) {
      return this.http.get(`${environment.API_URL}/users/` + id);
  }

  register(user: User) {
      return this.http.post(`${environment.API_URL}/users/register`, user);
  }

  update(user: User) {
      return this.http.put(`${environment.API_URL}/users/` + user.id, user);
  }

  delete(id: number) {
      return this.http.delete(`${environment.API_URL}/users/` + id);
  }
}
