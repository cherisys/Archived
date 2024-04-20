import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from '../models/User';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  apiUrl = "http://localhost:26012";
  constructor(private http:HttpClient) { }

  getAll() {
    return this.http.get<User[]>(`${this.apiUrl}/users`);
  }
}
