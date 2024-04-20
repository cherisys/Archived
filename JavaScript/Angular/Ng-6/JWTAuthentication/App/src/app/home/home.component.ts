import { Component, OnInit } from '@angular/core';
import { UserService } from '../services/user.service';
import { User } from '../models/User';
import { first } from 'rxjs/operators';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  users: User[] = [];

    constructor(private userService: UserService) {}

    ngOnInit() {
        this.userService.getUsers().pipe(first()).subscribe(users => { 
            this.users = users; 
        });
    }

}
